using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public Text goldDisplay;
    public Text goldCollectDisplay;
    public Text pollutionDisplay;
    public Text energyDisplay;
    public Text treeCostDisplay;
    public Text buildingCostDisplay;
    public Text reactorCostDisplay;
    public Text solarPlantCostDisplay;
    public Text coalPlantCostDisplay;
    public Text buildingCountDisplay;
    public Text treeCountDisplay;
    public Text reactorCountDisplay;
    public Text solarPlantCountDisplay;
    public Text coalPlantCountDisplay;
    public CustomCursor customCursor;

    [SerializeField] StatManager statManager;
    [SerializeField] Buttons buttons;
    [SerializeField] GenerateTiles generateTiles;

    [HideInInspector] public List<Tree> trees;

    [HideInInspector] public List<Building> buildings;

    [HideInInspector] public List<Reactor> reactors;

    [HideInInspector] public List<SolarPlant> solarPlants;

    [HideInInspector] public List<CoalPlant> coalPlants;
    
    [HideInInspector] public Tree treeToPlace;

    [HideInInspector] public Building buildingToPlace;

    [HideInInspector] public Reactor reactorToPlace;

    [HideInInspector] public SolarPlant solarPlantToPlace;

    [HideInInspector] public CoalPlant coalPlantToPlace;

    public Tile[] tiles = new Tile[]{};

    public void Update()
    {
        while (statManager.energyBeingUsed > statManager.energyBeingProvided)
        {
            buildings.Last(building => building.buildingEnabled).buildingEnabled = false;
            statManager.energyBeingUsed--;
        }
        foreach (Building building in buildings)
        {
            if (!building.buildingEnabled)
            {
                if (statManager.energyBeingUsed < statManager.energyBeingProvided)
                {
                    building.buildingEnabled = true;
                    statManager.energyBeingUsed++;
                }
            }
        }

        goldDisplay.text = statManager.Gold.ToString();
        goldCollectDisplay.text = statManager.inCollectionGold.ToString() + " / " + statManager.maxCollectionGold.ToString();
        pollutionDisplay.text = Mathf.Clamp(statManager.pollutionModifier, 0, 9999999).ToString();
        energyDisplay.text = statManager.energyBeingUsed.ToString() + " / " + statManager.energyBeingProvided.ToString();

        treeCostDisplay.text = statManager.treeCost.ToString();
        buildingCostDisplay.text = statManager.buildingCost.ToString();
        reactorCostDisplay.text = statManager.reactorCost.ToString();
        solarPlantCostDisplay.text = statManager.solarPlantCost.ToString();
        coalPlantCostDisplay.text = statManager.coalPlantCost.ToString();

        treeCountDisplay.text = statManager.treeCount.ToString();
        buildingCountDisplay.text = statManager.buildingCount.ToString();
        reactorCountDisplay.text = statManager.reactorCount.ToString();
        solarPlantCountDisplay.text = statManager.solarPlantCount.ToString();
        coalPlantCountDisplay.text = statManager.coalPlantCount.ToString();

        PlaceStuff();
    }
    public void PlaceStuff()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectPlaceTree();
            DetectPlaceBuilding();
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
        if (nearestTile.isOccupied)
        {
            return;
        }
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
            buttons.isMoved = true;
            buttons.FinishTreeMove();
            buttons.buildingInMovement = "";
        }
        else
        {
            statManager.treeCount--;
        }
    } 
    public void DetectPlaceBuilding()
    {
        if (buildingToPlace == null)
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
        if (!nearestTile.isOccupied)
        {
            Vector3 positionToPlace = nearestTile.transform.position;
            var placedBuilding = Instantiate(buildingToPlace, positionToPlace, Quaternion.identity);
            nearestTile.currentBuilding = placedBuilding;
            placedBuilding.placedTile = nearestTile;
            nearestTile.isOccupied = true;
            buildingToPlace = null;
            customCursor.gameObject.SetActive(false);
            Cursor.visible = true;
            buttons.isPlacing = false;
            buildings.Add(placedBuilding);
            if (buttons.isMoving)
            {
                buttons.isMoving = false;
                buttons.isMoved = true;
                buttons.FinishBuildingMove();
                buttons.buildingInMovement = "";
            }
            else
            {
                statManager.buildingCount--;
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
        if (!nearestTile.isOccupied && !nearestTileX1.isOccupied && !nearestTileY1.isOccupied && !nearestTileXY1.isOccupied)
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
                buttons.isMoved = true;
                buttons.FinishReactorMove();
                buttons.buildingInMovement = "";
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
        if (!nearestTile.isOccupied)
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
                buttons.isMoved = true;
                buttons.FinishSolarPlantMove();
                buttons.buildingInMovement = "";
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
        if (!nearestTile.isOccupied)
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
                buttons.isMoved = true;
                buttons.FinishCoalPlantMove();
                buttons.buildingInMovement = "";
            }
            else
            {
                statManager.coalPlantCount--;
            }
        }
    }
}
