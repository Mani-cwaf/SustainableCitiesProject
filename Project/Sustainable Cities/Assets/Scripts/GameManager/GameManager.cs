using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text goldDisplay;
    public Text goldCollectDisplay;
    public Text moralTextDisplay;
    public Text pollutionDisplay;
    public Text energyDisplay;
    public Text treeCostDisplay;
    public Text houseCostDisplay;
    public Text reactorCostDisplay;
    public Text solarPlantCostDisplay;
    public Text coalPlantCostDisplay;
    public Text houseCountDisplay;
    public Text treeCountDisplay;
    public Text reactorCountDisplay;
    public Text solarPlantCountDisplay;
    public Text coalPlantCountDisplay;
    public CustomCursor customCursor;

    public Sprite moralScaleMad;
    public Sprite moralScaleSad;
    public Sprite moralScaleNeutral;
    public Sprite moralScaleSlightlyHappy;
    public Sprite moralScaleHappy;
    public GameObject moralDisplayIcon;

    [SerializeField] StatManager statManager;
    [SerializeField] AnimationsManager animationsManager;
    [SerializeField] Buttons buttons;
    [SerializeField] GenerateTiles generateTiles;

    [HideInInspector] public List<Tree> trees;
    public List<House> houses;
    [HideInInspector] public List<Reactor> reactors;
    [HideInInspector] public List<SolarPlant> solarPlants;
    [HideInInspector] public List<CoalPlant> coalPlants;

    [HideInInspector] public Tree treeToPlace;
    [HideInInspector] public House houseToPlace;
    [HideInInspector] public Reactor reactorToPlace;
    [HideInInspector] public SolarPlant solarPlantToPlace;
    [HideInInspector] public CoalPlant coalPlantToPlace;

    public Tile[] tiles = new Tile[] { };

    public void Update()
    {
        foreach (House House in houses)
        {
            if (House)
            {
                if (!House.houseEnabled)
                {
                    if (statManager.energyBeingUsed <= statManager.energyBeingProvided)
                    {
                        House.houseEnabled = true;
                        statManager.energyBeingUsed++;
                        animationsManager.HouseEnabledAnimation(House);
                    }
                }
                if (House.houseEnabled)
                {
                    if (statManager.energyBeingProvided < statManager.energyBeingUsed)
                    {
                        House.houseEnabled = false;
                        statManager.energyBeingUsed--;
                        animationsManager.HouseDisabledAnimation(House);
                    }
                }
            }
        }
        if (statManager.moralModifier >= 50)
        {
            moralDisplayIcon.GetComponent<Image>().sprite = moralScaleHappy;
            moralTextDisplay.color = Color.green;
        }
        if (statManager.moralModifier > 0 && statManager.moralModifier < 50)
        {
            moralDisplayIcon.GetComponent<Image>().sprite = moralScaleSlightlyHappy;
            moralTextDisplay.color = new Color(0.4f, 1, 0.4f, 255);
        }
        if (statManager.moralModifier == 0)
        {
            moralDisplayIcon.GetComponent<Image>().sprite = moralScaleNeutral;
            moralTextDisplay.color = Color.yellow;
        }
        if (statManager.moralModifier < 0)
        {
            moralDisplayIcon.GetComponent<Image>().sprite = moralScaleSad;
            moralTextDisplay.color = new Color(1, 0.4f, 0.4f, 255);
        }
        if (statManager.moralModifier <= -50)
        {
            moralDisplayIcon.GetComponent<Image>().sprite = moralScaleMad;
            moralTextDisplay.color = Color.red;
        }

        goldDisplay.text = statManager.Gold.ToString();
        goldCollectDisplay.text = statManager.inCollectionGold.ToString() + " / " + statManager.maxCollectionGold.ToString();
        pollutionDisplay.text = Mathf.Clamp(statManager.pollutionModifier, 0, 9999999).ToString();
        energyDisplay.text = statManager.energyBeingUsed.ToString() + " / " + statManager.energyBeingProvided.ToString();
        moralTextDisplay.text = statManager.moralModifier.ToString();

        treeCostDisplay.text = statManager.treeCost.ToString();
        houseCostDisplay.text = statManager.houseCost.ToString();
        reactorCostDisplay.text = statManager.reactorCost.ToString();
        solarPlantCostDisplay.text = statManager.solarPlantCost.ToString();
        coalPlantCostDisplay.text = statManager.coalPlantCost.ToString();

        treeCountDisplay.text = statManager.treeCount.ToString();
        houseCountDisplay.text = statManager.houseCount.ToString();
        reactorCountDisplay.text = statManager.reactorCount.ToString();
        solarPlantCountDisplay.text = statManager.solarPlantCount.ToString();
        coalPlantCountDisplay.text = statManager.coalPlantCount.ToString();

        PlaceStuff();
    }
    public void PlaceStuff()
    {
        if (Input.GetMouseButton(0))
        {
            DetectPlaceTree();
            DetectPlaceHouse();
            DetectPlaceReactor();
            DetectPlaceSolarPlant();
            DetectPlaceCoalPlant();
        }
    }
    public void DetectPlaceTree()
    {
        if (treeToPlace == null)
        {
            return;
        }
        Tile nearestTile = null;
        float shortestDistance = float.MaxValue;
        foreach (Tile tile in tiles)
        {
            float distance = Vector2.Distance(tile.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestTile = tile;
            }
        }
        if (nearestTile && !nearestTile.isOccupied && shortestDistance < 2)
        {
            Vector3 positionToPlace = nearestTile.transform.position;
            var placedTree = Instantiate(treeToPlace, positionToPlace, Quaternion.identity);
            nearestTile.currentTree = placedTree;
            placedTree.placedTile = nearestTile;
            nearestTile.isOccupied = true;
            treeToPlace = null;
            customCursor.gameObject.SetActive(false);
            Cursor.visible = true;
            buttons.isPlacing = false;
            if (buttons.isMoving)
            {
                buttons.isMoving = false;
                buttons.Invoke(nameof(buttons.FinishTreeMove), 0.1f);
                buttons.houseInMovement = "";
            }
            else
            {
                statManager.treeCount--;
            }
        }
    }
    public void DetectPlaceHouse()
    {
        if (houseToPlace == null)
        {
            return;
        }
        Tile nearestTile = null;
        float shortestDistance = float.MaxValue;
        foreach (Tile tile in tiles)
        {
            float distance = Vector2.Distance(tile.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestTile = tile;
            }
        }
        if (nearestTile && !nearestTile.isOccupied && shortestDistance < 2)
        {
            Vector3 positionToPlace = nearestTile.transform.position;
            var placedHouse = Instantiate(houseToPlace, positionToPlace, Quaternion.identity);
            nearestTile.currentHouse = placedHouse;
            placedHouse.placedTile = nearestTile;
            nearestTile.isOccupied = true;
            houseToPlace = null;
            customCursor.gameObject.SetActive(false);
            Cursor.visible = true;
            buttons.isPlacing = false;
            houses.Insert(0, placedHouse);
            if (buttons.isMoving)
            {
                buttons.isMoving = false;
                buttons.Invoke(nameof(buttons.FinishHouseMove), 0.1f);
                buttons.houseInMovement = "";
            }
            else
            {
                statManager.houseCount--;
            }
        }
    }
    public void DetectPlaceReactor()
    {
        if (reactorToPlace == null)
        {
            return;
        }
        Tile nearestTile = null;
        float shortestDistance = float.MaxValue;
        foreach (Tile tile in tiles)
        {
            float distance = Vector2.Distance(tile.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestTile = tile;
            }
        }
        Vector3Int nearestTileIndex = generateTiles.TilePoses.GetValueOrDefault(nearestTile);
        Vector3Int nearestTileX1Index = new Vector3Int(nearestTileIndex.x + 1, nearestTileIndex.y);
        Vector3Int nearestTileY1Index = new Vector3Int(nearestTileIndex.x, nearestTileIndex.y + 1);
        Vector3Int nearestTileXY1Index = new Vector3Int(nearestTileIndex.x + 1, nearestTileIndex.y + 1);
        Tile nearestTileX1 = generateTiles.TilePosesInverse.GetValueOrDefault(nearestTileX1Index);
        Tile nearestTileY1 = generateTiles.TilePosesInverse.GetValueOrDefault(nearestTileY1Index);
        Tile nearestTileXY1 = generateTiles.TilePosesInverse.GetValueOrDefault(nearestTileXY1Index);
        if (nearestTile && nearestTileX1 && nearestTileY1 && nearestTileXY1 && !nearestTile.isOccupied && !nearestTileX1.isOccupied && !nearestTileY1.isOccupied && !nearestTileXY1.isOccupied && shortestDistance < 2)
        {
            Vector3 positionToPlace = nearestTile.transform.position;
            var placedReactor = Instantiate(reactorToPlace, positionToPlace, Quaternion.identity);
            nearestTile.isOccupied = true;
            nearestTileX1.isOccupied = true;
            nearestTileY1.isOccupied = true;
            nearestTileXY1.isOccupied = true;
            nearestTile.currentReactor = placedReactor;
            nearestTileX1.currentReactor = placedReactor;
            nearestTileY1.currentReactor = placedReactor;
            nearestTileXY1.currentReactor = placedReactor;
            placedReactor.placedTile = nearestTile;
            placedReactor.placedTileX1 = nearestTileX1;
            placedReactor.placedTileY1 = nearestTileY1;
            placedReactor.placedTileXY1 = nearestTileXY1;
            reactorToPlace = null;
            customCursor.gameObject.SetActive(false);
            Cursor.visible = true;
            buttons.isPlacing = false;
            if (buttons.isMoving)
            {
                buttons.isMoving = false;
                buttons.Invoke(nameof(buttons.FinishReactorMove), 0.1f);
                buttons.houseInMovement = "";
            }
            else
            {
                statManager.reactorCount--;
            }
        }
    }
    public void DetectPlaceSolarPlant()
    {
        if (solarPlantToPlace == null)
        {
            return;
        }
        Tile nearestTile = null;
        float shortestDistance = float.MaxValue;
        foreach (Tile tile in tiles)
        {
            float distance = Vector2.Distance(tile.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestTile = tile;
            }
        }
        if (nearestTile && !nearestTile.isOccupied && shortestDistance < 2)
        {
            Vector3 positionToPlace = nearestTile.transform.position;
            positionToPlace.x += 0.1f;
            var placedSolarPlant = Instantiate(solarPlantToPlace, positionToPlace, Quaternion.identity);
            nearestTile.currentSolarPlant = placedSolarPlant;
            placedSolarPlant.placedTile = nearestTile;
            nearestTile.isOccupied = true;
            solarPlantToPlace = null;
            customCursor.gameObject.SetActive(false);
            Cursor.visible = true;
            buttons.isPlacing = false;
            if (buttons.isMoving)
            {
                buttons.isMoving = false;
                buttons.Invoke(nameof(buttons.FinishSolarPlantMove), 0.1f);
                buttons.houseInMovement = "";
            }
            else
            {
                statManager.solarPlantCount--;
            }
        }
    }
    public void DetectPlaceCoalPlant()
    {
        if (coalPlantToPlace == null)
        {
            return;
        }
        Tile nearestTile = null;
        float shortestDistance = float.MaxValue;
        foreach (Tile tile in tiles)
        {
            float distance = Vector2.Distance(tile.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestTile = tile;
            }
        }
        if (!nearestTile.isOccupied && !nearestTile.isOccupied && shortestDistance < 2)
        {
            Vector3 positionToPlace = nearestTile.transform.position;
            positionToPlace.x += 0.1f;
            var placedCoalPlant = Instantiate(coalPlantToPlace, positionToPlace, Quaternion.identity);
            nearestTile.currentCoalPlant = placedCoalPlant;
            placedCoalPlant.placedTile = nearestTile;
            nearestTile.isOccupied = true;
            coalPlantToPlace = null;
            customCursor.gameObject.SetActive(false);
            Cursor.visible = true;
            buttons.isPlacing = false;
            if (buttons.isMoving)
            {
                buttons.isMoving = false;
                buttons.Invoke(nameof(buttons.FinishCoalPlantMove), 0.1f);
                buttons.houseInMovement = "";
            }
            else
            {
                statManager.coalPlantCount--;
            }
        }
    }
}
