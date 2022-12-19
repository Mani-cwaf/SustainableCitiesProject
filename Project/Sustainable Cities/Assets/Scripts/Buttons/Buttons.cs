using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [SerializeField] GameObject mainCameraObject;
    [SerializeField] GameManager gameManager;
    [SerializeField] StatManager statManager;
    public GameObject cityObjects;
    public GameObject shopObjects;
    public GameObject mainMenuObjects;
    public List<GameObject> gloabalObjects;

    public GameObject city;
    public GameObject shop;
    public GameObject mainMenu;

    public Tree treePrefab;
    public House housePrefab;
    public Reactor reactorPrefab;
    public SolarPlant solarPlantPrefab;
    public CoalPlant coalPlantPrefab;

    [SerializeField] GameObject selectMenu;
    [SerializeField] GameObject renameMenu;
    [SerializeField] TMP_InputField renameMenuText;
    [HideInInspector] public Tree selectedTree;
    [HideInInspector] public House selectedHouse;
    [HideInInspector] public Reactor selectedReactor;
    [HideInInspector] public CoalPlant selectedCoalPlant;
    [HideInInspector] public SolarPlant selectedSolarPlant;

    public Text editText;

    [HideInInspector] public bool isSelected;
    [HideInInspector] public bool isRenaming;
    [HideInInspector] public bool isPlacing;
    [HideInInspector] public bool isMoving;

    public string houseInMovement;

    public string sceneName = "city";
    public void Start()
    {
        cityObjects.SetActive(false);
        shopObjects.SetActive(false);
        gloabalObjects.ForEach(gloabalObject => gloabalObject.SetActive(false));
        mainMenuObjects.SetActive(true);
    }
    public void CollectCash()
    {
        if (statManager.inCollectionGold > 0)
        {
            statManager.Gold += statManager.inCollectionGold;
            statManager.inCollectionGold = 0;
        }
    }
    public void CancelTreePlace()
    {
        if (gameManager.treeToPlace != null)
        {
            gameManager.treeToPlace = null;
            gameManager.customCursor.gameObject.SetActive(false);
            Cursor.visible = true;
            isPlacing = false;
        }
    }
    public void CancelHousePlace()
    {
        if (gameManager.houseToPlace != null)
        {
            gameManager.houseToPlace = null;
            gameManager.customCursor.gameObject.SetActive(false);
            Cursor.visible = true;
            isPlacing = false;
        }
    }
    public void CancelReactorPlace()
    {
        if (gameManager.reactorToPlace != null)
        {
            gameManager.reactorToPlace = null;
            gameManager.customCursor.gameObject.SetActive(false);
            Cursor.visible = true;
            isPlacing = false;
        }
    }
    public void CancelSolarPlantPlace()
    {
        if (gameManager.solarPlantToPlace != null)
        {
            gameManager.solarPlantToPlace = null;
            gameManager.customCursor.gameObject.SetActive(false);
            Cursor.visible = true;
            isPlacing = false;
        }
    }
    public void CancelCoalPlantPlace()
    {
        if (gameManager.coalPlantToPlace != null)
        {
            gameManager.coalPlantToPlace = null;
            gameManager.customCursor.gameObject.SetActive(false);
            Cursor.visible = true;
            isPlacing = false;
        }
    }
    public void BuyTree()
    {
        if (statManager.Gold >= statManager.treeCost)
        {
            statManager.Gold -= statManager.treeCost;
            statManager.treeCount++;
        }
    }
    public void BuyHouse()
    {
        if (statManager.Gold >= statManager.houseCost)
        {
            statManager.Gold -= statManager.houseCost;
            statManager.houseCount++;
        }
    }
    public void BuyReactor()
    {
        if (statManager.Gold >= statManager.reactorCost)
        {
            statManager.Gold -= statManager.reactorCost;
            statManager.reactorCount++;
        }
    }
    public void BuySolarPlant()
    {
        if (statManager.Gold >= statManager.solarPlantCost)
        {
            statManager.Gold -= statManager.solarPlantCost;
            statManager.solarPlantCount++;
        }
    }
    public void BuyCoalPlant()
    {
        if (statManager.Gold >= statManager.coalPlantCost)
        {
            statManager.Gold -= statManager.coalPlantCost; 
            statManager.coalPlantCount++;
        }
    }
    public void RenameTreeFinish()
    {
        if (sceneName == "city" && isSelected)
        {
            renameMenu.SetActive(false);
            selectedTree.displayName = renameMenuText.textComponent.text;
        }
    }
    public void RenameHouseFinish()
    {
        if (sceneName == "city" && isSelected)
        {
            renameMenu.SetActive(false);
            selectedHouse.displayName = renameMenuText.textComponent.text;
        }
    }
    public void RenameReactorFinish()
    {
        if (sceneName == "city" && isSelected)
        {
            renameMenu.SetActive(false);
            selectedReactor.displayName = renameMenuText.textComponent.text;
        }
    }
    public void RenameSolarPlantFinish()
    {
        if (sceneName == "city" && isSelected)
        {
            renameMenu.SetActive(false);
            selectedHouse.displayName = renameMenuText.textComponent.text;
        }
    }
    public void RenameCoalPlantFinish()
    {
        if (sceneName == "city" && isSelected)
        {
            renameMenu.SetActive(false);
            selectedCoalPlant.displayName = renameMenuText.textComponent.text;
        }
    }
    public void PlaceTree(Tree tree)
    {
        if (sceneName == "city")
        {
            if (statManager.treeCount > 0 && !gameManager.treeToPlace)
            {
                isPlacing = true;
                gameManager.treeToPlace = tree;
                gameManager.customCursor.gameObject.SetActive(true);
                gameManager.customCursor.transform.localScale = new Vector3(1.309f, 1.309f);
                gameManager.customCursor.GetComponent<SpriteRenderer>().sprite = tree.GetComponent<SpriteRenderer>().sprite;
                Cursor.visible = false;
            }
        }
    }
    public void PlaceHouse(House house)
    {
        if (sceneName == "city")
        {
            if (statManager.houseCount > 0  && !gameManager.houseToPlace)
            {
                isPlacing = true;
                gameManager.houseToPlace = house;
                gameManager.customCursor.gameObject.SetActive(true);
                gameManager.customCursor.transform.localScale = new Vector3(0.35f, 0.35f);
                gameManager.customCursor.GetComponent<SpriteRenderer>().sprite = house.GetComponent<SpriteRenderer>().sprite;
                Cursor.visible = false;
            }
        }
    }
    public void PlaceReactor(Reactor reactor)
    {
        if (sceneName == "city")
        {
            if (statManager.reactorCount > 0 && !gameManager.reactorToPlace)
            {
                isPlacing = true;
                gameManager.reactorToPlace = reactor;
                gameManager.customCursor.gameObject.SetActive(true);
                gameManager.customCursor.transform.localScale = new Vector3(0.73f, 0.828f);
                gameManager.customCursor.GetComponent<SpriteRenderer>().sprite = reactor.GetComponent<SpriteRenderer>().sprite;
                Cursor.visible = false;
            }
        }
    }
    public void PlaceSolarPlant(SolarPlant solarPlant)
    {
        if (sceneName == "city")
        {
            if (statManager.solarPlantCount > 0 && !gameManager.solarPlantToPlace)
            {
                isPlacing = true;
                gameManager.solarPlantToPlace = solarPlant;
                gameManager.customCursor.gameObject.SetActive(true);
                gameManager.customCursor.transform.localScale = new Vector3(0.1937243f, 0.1937243f);
                gameManager.customCursor.GetComponent<SpriteRenderer>().sprite = solarPlant.GetComponent<SpriteRenderer>().sprite;
                Cursor.visible = false;
            }
        }
    }
    public void PlaceCoalPlant(CoalPlant coalPlant)
    {
        if (sceneName == "city")
        {
            if (statManager.coalPlantCount > 0 && !gameManager.coalPlantToPlace)
            {
                isPlacing = true;
                gameManager.coalPlantToPlace = coalPlant;
                gameManager.customCursor.gameObject.SetActive(true);
                gameManager.customCursor.transform.localScale = new Vector3(0.1937243f, 0.1937243f);
                gameManager.customCursor.GetComponent<SpriteRenderer>().sprite = coalPlant.GetComponent<SpriteRenderer>().sprite;
                Cursor.visible = false;
            }
        }
    }
    void MoveTree()
    {
        if (sceneName == "city")
        {
            selectMenu.SetActive(false);
            isPlacing = true;
            gameManager.treeToPlace = selectedTree;
            gameManager.customCursor.gameObject.SetActive(true);
            gameManager.customCursor.transform.localScale = new Vector3(1.309f, 1.309f);
            gameManager.customCursor.GetComponent<SpriteRenderer>().sprite = selectedTree.GetComponent<SpriteRenderer>().sprite;
            Cursor.visible = false;
            isMoving = true;
        }
    }
    void MoveHouse()
    {
        if (sceneName == "city")
        {
            selectMenu.SetActive(false);
            isPlacing = true;
            gameManager.houseToPlace = selectedHouse;
            gameManager.customCursor.gameObject.SetActive(true);
            gameManager.customCursor.transform.localScale = new Vector3(0.35f, 0.35f);
            gameManager.customCursor.GetComponent<SpriteRenderer>().sprite = selectedHouse.GetComponent<SpriteRenderer>().sprite;
            Cursor.visible = false;
            isMoving = true;
        }
    }
    void MoveReactor()
    {
        if (sceneName == "city")
        {
            selectMenu.SetActive(false);
            isPlacing = true;
            gameManager.reactorToPlace = selectedReactor;
            gameManager.customCursor.gameObject.SetActive(true);
            gameManager.customCursor.transform.localScale = new Vector3(0.73f, 0.828f);
            gameManager.customCursor.GetComponent<SpriteRenderer>().sprite = selectedReactor.GetComponent<SpriteRenderer>().sprite;
            Cursor.visible = false;
            isMoving = true;
        }
    }
    void MoveSolarPlant()
    {
        if (sceneName == "city")
        {
            selectMenu.SetActive(false);
            isPlacing = true;
            gameManager.solarPlantToPlace = selectedSolarPlant;
            gameManager.customCursor.gameObject.SetActive(true);
            gameManager.customCursor.transform.localScale = new Vector3(0.1937243f, 0.1937243f);
            gameManager.customCursor.GetComponent<SpriteRenderer>().sprite = selectedSolarPlant.GetComponent<SpriteRenderer>().sprite;
            Cursor.visible = false;
            isMoving = true;
        }
    }
    void MoveCoalPlant()
    {
        if (sceneName == "city")
        {
            selectMenu.SetActive(false);
            isPlacing = true;
            gameManager.coalPlantToPlace = selectedCoalPlant;
            gameManager.customCursor.gameObject.SetActive(true);
            gameManager.customCursor.transform.localScale = new Vector3(0.1937243f, 0.1937243f);
            gameManager.customCursor.GetComponent<SpriteRenderer>().sprite = selectedCoalPlant.GetComponent<SpriteRenderer>().sprite;
            Cursor.visible = false;
            isMoving = true;
        }
    }
    public void ToMainMenu()
    {
        mainCameraObject.transform.position = mainMenu.transform.position;
        cityObjects.SetActive(false);
        shopObjects.SetActive(false);
        gloabalObjects.ForEach(gloabalObject => gloabalObject.SetActive(false));
        mainMenuObjects.SetActive(true);
        sceneName = "options";
    }
    public void ToCity()
    {
        mainCameraObject.transform.position = city.transform.position;
        shopObjects.SetActive(false);
        mainMenuObjects.SetActive(false);
        gloabalObjects.ForEach(gloabalObject => gloabalObject.SetActive(true));
        cityObjects.SetActive(true);
        sceneName = "city";
    }
    public void ToShop()
    {
        mainCameraObject.transform.position = shop.transform.position;
        cityObjects.SetActive(false);
        mainMenuObjects.SetActive(false);
        gloabalObjects.ForEach(gloabalObject => gloabalObject.SetActive(true));
        shopObjects.SetActive(true);
        sceneName = "shop";
    }
    public void SelectTree(Tree tree)
    {
        if (!isPlacing && !isSelected)
        {
            isSelected = true;
            Invoke(nameof(SelectTreeCoRoutine), 0.1f);
            selectedTree = tree;
        }
    }
    public void SellTree()
    {
        statManager.pollutionModifier += statManager.reactorPollutionEffect;
        statManager.moralModifier -= statManager.treeMoralEffect;
        statManager.Gold += statManager.treeCost;
        selectedTree.placedTile.isOccupied = false;
        Destroy(selectedTree.gameObject);
        selectedTree = null;
        selectMenu.SetActive(false);
        isSelected = false;
    }
    public void FinishTreeMove()
    {
        statManager.Gold -= statManager.treeCost;
        SellTree();
    }
    public void SelectHouse(House house)
    {
        if (!isPlacing && !isSelected)
        {
            isSelected = true;
            Invoke(nameof(SelectHouseCoRoutine), 0.1f);
            selectedHouse = house;
        }
    }
    public void SellHouse()
    {
        if (selectedHouse.houseEnabled) statManager.energyBeingUsed--;
        statManager.maxCollectionGold -= selectedHouse.houseMaxCollectionsGoldIncrease;
        statManager.Gold += statManager.houseCost;
        selectedHouse.placedTile.isOccupied = false;
        gameManager.houses.Remove(selectedHouse);
        Destroy(selectedHouse.gameObject);
        selectedHouse = null;
        selectMenu.SetActive(false);
        isSelected = false;
    }
    public void FinishHouseMove()
    {
        statManager.Gold -= statManager.houseCost;
        SellHouse();
    }
    public void SelectReactor(Reactor reactor)
    {
        if (!isPlacing && !isSelected)
        {
            isSelected = true;
            Invoke(nameof(SelectReactorCoRoutine), 0.1f);
            selectedReactor = reactor;
        }
    }
    public void SellReactor()
    {
        statManager.Gold += statManager.reactorCost;
        statManager.energyBeingProvided -= statManager.reactorEnergyBeingProvided;
        statManager.pollutionModifier -= statManager.reactorPollutionEffect;
        statManager.moralModifier += statManager.reactorMoralEffect;
        selectedReactor.placedTile.isOccupied = false;
        selectedReactor.placedTileX1.isOccupied = false;
        selectedReactor.placedTileY1.isOccupied = false;
        selectedReactor.placedTileXY1.isOccupied = false;
        Destroy(selectedReactor.gameObject);
        selectedReactor = null;
        selectMenu.SetActive(false);
        isSelected = false;
    }
    public void FinishReactorMove()
    {
        statManager.Gold -= statManager.reactorCost;
        SellReactor();
    }
    public void SelectSolarPlant(SolarPlant solarPlant)
    {
        if (!isPlacing && !isSelected)
        {
            isSelected = true;
            Invoke(nameof(SelectSolarPlantCoRoutine), 0.1f);
            selectedSolarPlant = solarPlant;
        }
    }
    public void SellSolarPlant()
    {
        statManager.Gold += statManager.solarPlantCost;
        statManager.energyBeingProvided -= statManager.solarPlantEnergyBeingProvided;
        statManager.moralModifier -= statManager.solarPlantMoralEffect;
        selectedSolarPlant.placedTile.isOccupied = false;
        Destroy(selectedSolarPlant.gameObject);
        selectedSolarPlant = null;
        selectMenu.SetActive(false);
        isSelected = false;
    }
    public void FinishSolarPlantMove()
    {
        statManager.Gold -= statManager.solarPlantCost;
        SellSolarPlant();
    }
    public void SelectCoalPlant(CoalPlant coalPlant)
    {
        if (!isPlacing && !isSelected)
        {
            isSelected = true;
            Invoke(nameof(SelectCoalPlantCoRoutine), 0.1f);
            selectedCoalPlant = coalPlant;
        }
    }
    public void SellCoalPlant()
    {
        statManager.Gold += statManager.coalPlantCost;
        statManager.energyBeingProvided -= statManager.coalPlantEnergyBeingProvided;
        statManager.pollutionModifier -= statManager.coalPlantPollutionEffect;
        statManager.moralModifier += statManager.coalPlantMoralEffect;
        selectedCoalPlant.placedTile.isOccupied = false;
        Destroy(selectedCoalPlant.gameObject);
        selectedCoalPlant = null;
        selectMenu.SetActive(false);
        isSelected = false;
    }
    public void FinishCoalPlantMove()
    {
        statManager.Gold -= statManager.coalPlantCost;
        SellCoalPlant();
    }

    public void RemoveSelection()
    {
        selectMenu.SetActive(false);
        isSelected = false;
    }
    public void MoveStuff()
    {
        if (selectedTree)
        {
            MoveTree();
        }
        if (selectedHouse)
        {
            MoveHouse();
        }
        if (selectedReactor)
        {
            MoveReactor();
        }
        if (selectedSolarPlant)
        {
            MoveSolarPlant();
        }
        if (selectedCoalPlant)
        {
            MoveCoalPlant();
        }
    }
    public void SellStuff()
    {
        if (selectedTree)
        {
            SellTree();
        }
        if (selectedHouse)
        {
            SellHouse();
        }
        if (selectedReactor)
        {
            SellReactor();
        }
        if (selectedSolarPlant)
        {
            SellSolarPlant();
        }
        if (selectedCoalPlant)
        {
            SellCoalPlant();
        }
    }
    public void RenameStuff()
    {
        if (sceneName == "city" && isSelected)
        {
            renameMenu.SetActive(true);
            isRenaming = true;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isSelected && !isPlacing && !isRenaming)
        {
            RemoveSelection();
        }
        if (Input.GetKey(KeyCode.T) && !isRenaming)
        {
            PlaceTree(treePrefab);
        }
        if (Input.GetKey(KeyCode.B) && !isRenaming)
        {
            PlaceHouse(housePrefab);
        }
        if (Input.GetKey(KeyCode.R) && !isRenaming)
        {
            PlaceReactor(reactorPrefab);
        }
        if (Input.GetKey(KeyCode.S) && !isRenaming)
        {
            PlaceSolarPlant(solarPlantPrefab);
        }
        if (Input.GetKey(KeyCode.C) && !isRenaming)
        {
            PlaceCoalPlant(coalPlantPrefab);
        }
        if (Input.GetKeyDown(KeyCode.Return) && !isPlacing && !isMoving && isRenaming && selectedTree)
        {
            RenameTreeFinish();
        }
        if (Input.GetKeyDown(KeyCode.Return) && !isPlacing && !isMoving && isRenaming && selectedHouse)
        {
            RenameHouseFinish();
        }
        if (Input.GetKeyDown(KeyCode.Return) && !isPlacing && !isMoving && isRenaming && selectedReactor)
        {
            RenameReactorFinish();
        }
        if (Input.GetKeyDown(KeyCode.Return) && !isPlacing && !isMoving && isRenaming && selectedSolarPlant)
        {
            RenameSolarPlantFinish();
        }
        if (Input.GetKeyDown(KeyCode.Return) && !isPlacing && !isMoving && isRenaming && selectedCoalPlant)
        {
            RenameCoalPlantFinish();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && (!isSelected && isPlacing || (isSelected && isMoving)))
        {
            CancelHousePlace();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && (!isSelected && isPlacing || (isSelected && isMoving)))
        {
            CancelReactorPlace();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && (!isSelected && isPlacing || (isSelected && isMoving)))
        {
            CancelSolarPlantPlace();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && (!isSelected && isPlacing || (isSelected && isMoving)))
        {
            CancelCoalPlantPlace();
        }
        if (selectedTree != null)
        {
            editText.text = "Selected " + selectedTree.displayName;
        }
        if (selectedHouse != null)
        {
            editText.text = "Selected " + selectedHouse.displayName;
        }
        if (selectedReactor != null)
        {
            editText.text = "Selected " + selectedReactor.displayName;
        }
        if (selectedSolarPlant != null)
        {
            editText.text = "Selected " + selectedSolarPlant.displayName;
        }
        if (selectedCoalPlant != null)
        {
            editText.text = "Selected " + selectedCoalPlant.displayName;
        }
    }
    void SelectTreeCoRoutine()
    {
        selectMenu.SetActive(true);
    }
    void SelectHouseCoRoutine()
    {
        selectMenu.SetActive(true);
    }
    void SelectReactorCoRoutine()
    {
        selectMenu.SetActive(true);
    }
    void SelectSolarPlantCoRoutine()
    {
        selectMenu.SetActive(true);
    }
    void SelectCoalPlantCoRoutine()
    {
        selectMenu.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
