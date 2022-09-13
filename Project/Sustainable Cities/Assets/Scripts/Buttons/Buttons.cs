using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    public Building buildingPrefab;
    public Reactor reactorPrefab;
    public SolarPlant solarPlantPrefab;
    public CoalPlant coalPlantPrefab;

    [SerializeField] GameObject selectMenu;
    [SerializeField] GameObject selectMenuBack;
    [SerializeField] GameObject renameMenuTree;
    [SerializeField] GameObject renameMenuBuilding;
    [SerializeField] GameObject renameMenuReactor;
    [SerializeField] GameObject renameMenuSolarPlant;
    [SerializeField] GameObject renameMenuCoalPlant;
    [SerializeField] TMP_InputField renameMenuTextTree;
    [SerializeField] TMP_InputField renameMenuTextBuilding;
    [SerializeField] TMP_InputField renameMenuTextReactor;
    [SerializeField] TMP_InputField renameMenuTextSolarPlant;
    [SerializeField] TMP_InputField renameMenuTextCoalPlant;
    [SerializeField] GameObject selectMenuTree;
    [SerializeField] GameObject selectMenuBuilding;
    [SerializeField] GameObject selectMenuReactor;
    [SerializeField] GameObject selectMenuSolarPlant;
    [SerializeField] GameObject selectMenuCoalPlant;
    private List<GameObject> menuThings = new List<GameObject>();
    [HideInInspector] public Tree selectedTree;
    [HideInInspector] public Building selectedBuilding;
    [HideInInspector] public Reactor selectedReactor;
    [HideInInspector] public CoalPlant selectedCoalPlant;
    [HideInInspector] public SolarPlant selectedSolarPlant;

    public Text editText;

    [HideInInspector] public bool isSelected;
    [HideInInspector] public bool isRenaming;
    [HideInInspector] public bool isPlacing;
    [HideInInspector] public bool isMoving;
    [HideInInspector] public bool isMoved;

    public string buildingInMovement;

    public string sceneName = "city";
    public void Start()
    {
        cityObjects.SetActive(false);
        shopObjects.SetActive(false);
        gloabalObjects.ForEach(gloabalObject => gloabalObject.SetActive(false));
        mainMenuObjects.SetActive(true);
        menuThings = new List<GameObject>() { selectMenu, selectMenuBack, selectMenuTree, selectMenuBuilding, selectMenuReactor, selectMenuSolarPlant, selectMenuCoalPlant };
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
    public void CancelBuildingPlace()
    {
        if (gameManager.buildingToPlace != null)
        {
            gameManager.buildingToPlace = null;
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
    public void BuyBuilding()
    {
        if (statManager.Gold >= statManager.buildingCost)
        {
            statManager.Gold -= statManager.buildingCost;
            statManager.buildingCount++;
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
    public void RenameTree()
    {
        if (sceneName == "city" && isSelected)
        {
            renameMenuTree.SetActive(true);
            isRenaming = true;
        }
    }
    public void RenameBuilding()
    {
        if (sceneName == "city" && isSelected)
        {
            renameMenuBuilding.SetActive(true);
            isRenaming = true;
        }
    }
    public void RenameReactor()
    {
        if (sceneName == "city" && isSelected)
        {
            renameMenuReactor.SetActive(true);
            isRenaming = true;
        }
    }
    public void RenameSolarPlant()
    {
        if (sceneName == "city" && isSelected)
        {
            renameMenuSolarPlant.SetActive(true);
            isRenaming = true;
        }
    }
    public void RenameCoalPlant()
    {
        if (sceneName == "city" && isSelected)
        {
            renameMenuCoalPlant.SetActive(true);
            isRenaming = true;
        }
    }
    public void RenameTreeFinish()
    {
        if (sceneName == "city" && isSelected)
        {
            renameMenuTree.SetActive(false);
            selectedTree.displayName = renameMenuTextTree.textComponent.text;
        }
    }
    public void RenameBuildingFinish()
    {
        if (sceneName == "city" && isSelected)
        {
            renameMenuBuilding.SetActive(false);
            selectedBuilding.displayName = renameMenuTextBuilding.textComponent.text;
        }
    }
    public void RenameReactorFinish()
    {
        if (sceneName == "city" && isSelected)
        {
            renameMenuReactor.SetActive(false);
            selectedReactor.displayName = renameMenuTextReactor.textComponent.text;
        }
    }
    public void RenameSolarPlantFinish()
    {
        if (sceneName == "city" && isSelected)
        {
            renameMenuSolarPlant.SetActive(false);
            selectedBuilding.displayName = renameMenuTextBuilding.textComponent.text;
        }
    }
    public void RenameCoalPlantFinish()
    {
        if (sceneName == "city" && isSelected)
        {
            renameMenuCoalPlant.SetActive(false);
            selectedCoalPlant.displayName = renameMenuTextCoalPlant.textComponent.text;
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
    public void PlaceBuilding(Building building)
    {
        if (sceneName == "city")
        {
            if (statManager.buildingCount > 0  && !gameManager.buildingToPlace)
            {
                isPlacing = true;
                gameManager.buildingToPlace = building;
                gameManager.customCursor.gameObject.SetActive(true);
                gameManager.customCursor.transform.localScale = new Vector3(0.35f, 0.35f);
                gameManager.customCursor.GetComponent<SpriteRenderer>().sprite = building.GetComponent<SpriteRenderer>().sprite;
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
    void MoveTree(Tree tree)
    {
        if (sceneName == "city")
        {
            if (selectedTree && isMoving)
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
    void MoveBuilding(Building building)
    {
        if (sceneName == "city")
        {
            if (selectedBuilding && isMoving)
            {
                isPlacing = true;
                gameManager.buildingToPlace = building;
                gameManager.customCursor.gameObject.SetActive(true);
                gameManager.customCursor.transform.localScale = new Vector3(0.35f, 0.35f);
                gameManager.customCursor.GetComponent<SpriteRenderer>().sprite = building.GetComponent<SpriteRenderer>().sprite;
                Cursor.visible = false;
            }
        }
    }
    void MoveReactor(Reactor reactor)
    {
        if (sceneName == "city")
        {
            if (selectedReactor && isMoving)
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
    void MoveSolarPlant(SolarPlant solarPlant)
    {
        if (sceneName == "city")
        {
            if (selectedSolarPlant && isMoving)
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
    void MoveCoalPlant(CoalPlant coalPlant)
    {
        if (sceneName == "city")
        {
            if (selectedCoalPlant && isMoving)
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
        isSelected = true;
        Invoke("SelectTreeCoRoutine", 0.1f);
        selectedTree = tree;
        if (isPlacing)
        {
            CancelBuildingPlace();
            CancelReactorPlace();
            CancelSolarPlantPlace();
            CancelCoalPlantPlace();
        }
    }
    public void SellTree()
    {
        if (selectedTree != null)
        {
            statManager.pollutionModifier += statManager.reactorPollutionEffect;
            statManager.moralModifier -= statManager.treeMoralEffect;
            statManager.Gold += statManager.treeCost;
            selectedTree.placedTile.isOccupied = false;
            Destroy(selectedTree.gameObject);
            selectedTree = null;
            selectMenu.SetActive(false);
            selectMenuBack.SetActive(false);
            selectMenuTree.SetActive(false);
        }
    }
    public void MoveTree()
    {
        selectMenu.SetActive(false);
        selectMenuBack.SetActive(false);
        selectMenuTree.SetActive(false);
        isMoving = true;
        MoveTree(selectedTree);
    }
    public void FinishTreeMove()
    {
        statManager.pollutionModifier += statManager.reactorPollutionEffect;
        statManager.moralModifier -= statManager.treeMoralEffect;
        selectedTree.placedTile.isOccupied = false;
        Destroy(selectedTree.gameObject);
        isMoved = false;
    }
    public void SelectBuilding(Building building)
    {
        isSelected = true;
        Invoke("SelectBuildingCoRoutine", 0.1f);
        selectedBuilding = building;
        if (isPlacing)
        {
            CancelBuildingPlace();
            CancelReactorPlace();
            CancelSolarPlantPlace();
            CancelCoalPlantPlace();
        }
    }
    public void SellBuilding()
    {
        if (selectedBuilding != null)
        {
            statManager.energyBeingUsed -= statManager.buildingEnergyBeingUsed;
            statManager.maxCollectionGold -= selectedBuilding.houseMaxCollectionsGoldIncrease;
            statManager.Gold += statManager.buildingCost;
            selectedBuilding.placedTile.isOccupied = false;
            Destroy(selectedBuilding.gameObject);
            selectedBuilding = null;
            selectMenu.SetActive(false);
            selectMenuBack.SetActive(false);
            selectMenuBuilding.SetActive(false);
        }
    }
    public void MoveBuilding()
    {
        selectMenu.SetActive(false);
        selectMenuBack.SetActive(false);
        selectMenuBuilding.SetActive(false);
        isMoving = true;
        MoveBuilding(selectedBuilding);
    }
    public void FinishBuildingMove()
    {
        statManager.energyBeingUsed -= statManager.buildingEnergyBeingUsed;
        statManager.maxCollectionGold -= selectedBuilding.houseMaxCollectionsGoldIncrease;
        selectedBuilding.placedTile.isOccupied = false;
        Destroy(selectedBuilding.gameObject);
        isMoved = false;
    }
    public void SelectReactor(Reactor reactor)
    {
        isSelected = true;
        Invoke("SelectReactorCoRoutine", 0.1f);
        selectedReactor = reactor;
        if (isPlacing)
        {
            CancelBuildingPlace();
            CancelReactorPlace();
            CancelSolarPlantPlace();
            CancelCoalPlantPlace();
        }
    }
    public void SellReactor()
    {
        if (selectedReactor != null)
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
            selectMenuBack.SetActive(false);
            selectMenuReactor.SetActive(false);
        }
    }
    public void MoveReactor()
    {
        selectMenu.SetActive(false);
        selectMenuBack.SetActive(false);
        selectMenuReactor.SetActive(false);
        isMoving = true;
        MoveReactor(selectedReactor);
    }
    public void FinishReactorMove()
    {
        statManager.energyBeingProvided -= statManager.reactorEnergyBeingProvided;
        statManager.pollutionModifier -= statManager.reactorPollutionEffect;
        statManager.moralModifier += statManager.reactorMoralEffect;
        selectedReactor.placedTile.isOccupied = false;
        Destroy(selectedReactor.gameObject);
        isMoved = false;
    }
    public void SelectSolarPlant(SolarPlant solarPlant)
    {
        isSelected = true;
        Invoke("SelectSolarPlantCoRoutine", 0.1f);
        selectedSolarPlant = solarPlant;
        if (isPlacing)
        {
            CancelBuildingPlace();
            CancelReactorPlace();
            CancelSolarPlantPlace();
            CancelCoalPlantPlace();
        }
    }
    public void SellSolarPlant()
    {
        if (selectedSolarPlant != null)
        {
            statManager.Gold += statManager.solarPlantCost;
            statManager.energyBeingProvided -= statManager.solarPlantEnergyBeingProvided;
            statManager.moralModifier -= statManager.solarPlantMoralEffect;
            selectedSolarPlant.placedTile.isOccupied = false;
            Destroy(selectedSolarPlant.gameObject);
            selectedSolarPlant = null;
            selectMenu.SetActive(false);
            selectMenuBack.SetActive(false);
            selectMenuSolarPlant.SetActive(false);
        }
    }
    public void MoveSolarPlant()
    {
        selectMenu.SetActive(false);
        selectMenuBack.SetActive(false);
        selectMenuSolarPlant.SetActive(false);
        isMoving = true;
        MoveSolarPlant(selectedSolarPlant);
    }
    public void FinishSolarPlantMove()
    {
        statManager.energyBeingProvided -= statManager.solarPlantEnergyBeingProvided;
        statManager.moralModifier -= statManager.solarPlantMoralEffect;
        selectedSolarPlant.placedTile.isOccupied = false;
        Destroy(selectedSolarPlant.gameObject);
        isMoved = false;
    }
    public void SelectCoalPlant(CoalPlant coalPlant)
    {
        isSelected = true;
        Invoke("SelectCoalPlantCoRoutine", 0.1f);
        selectedCoalPlant = coalPlant;
        if (isPlacing)
        {
            CancelBuildingPlace();
            CancelReactorPlace();
            CancelSolarPlantPlace();
            CancelCoalPlantPlace();
        }
    }
    public void SellCoalPlant()
    {
        if (selectedCoalPlant != null)
        {
            statManager.Gold += statManager.coalPlantCost;
            statManager.energyBeingProvided -= statManager.coalPlantEnergyBeingProvided;
            statManager.pollutionModifier -= statManager.coalPlantPollutionEffect;
            statManager.moralModifier += statManager.coalPlantMoralEffect;
            selectedCoalPlant.placedTile.isOccupied = false;
            Destroy(selectedCoalPlant.gameObject);
            selectedCoalPlant = null;
            selectMenu.SetActive(false);
            selectMenuBack.SetActive(false);
            selectMenuCoalPlant.SetActive(false);
        }
    }
    public void MoveCoalPlant()
    {
        selectMenu.SetActive(false);
        selectMenuBack.SetActive(false);
        selectMenuCoalPlant.SetActive(false);
        isMoving = true;
        MoveCoalPlant(selectedCoalPlant);
    }
    public void FinishCoalPlantMove()
    {
        statManager.energyBeingProvided -= statManager.coalPlantEnergyBeingProvided;
        statManager.pollutionModifier -= statManager.coalPlantPollutionEffect;
        statManager.moralModifier += statManager.coalPlantMoralEffect;
        selectedCoalPlant.placedTile.isOccupied = false;
        Destroy(selectedCoalPlant.gameObject);
        isMoved = false;
    }

    public void RemoveSelection()
    {
        foreach (GameObject menuThing in menuThings)
        {
            menuThing.SetActive(false);
        }
        isSelected = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isSelected && !isPlacing && !isRenaming)
        {
            RemoveSelection();
        }
        if (Input.GetKeyDown(KeyCode.T) && !isRenaming)
        {
            PlaceTree(treePrefab);
        }
        if (Input.GetKeyDown(KeyCode.B) && !isRenaming)
        {
            PlaceBuilding(buildingPrefab);
        }
        if (Input.GetKeyDown(KeyCode.R) && !isRenaming)
        {
            PlaceReactor(reactorPrefab);
        }
        if (Input.GetKeyDown(KeyCode.S) && !isRenaming)
        {
            PlaceSolarPlant(solarPlantPrefab);
        }
        if (Input.GetKeyDown(KeyCode.C) && !isRenaming)
        {
            PlaceCoalPlant(coalPlantPrefab);
        }
        if (Input.GetKeyDown(KeyCode.Return) && !isPlacing && !isMoving && isRenaming && selectedTree)
        {
            RenameTreeFinish();
        }
        if (Input.GetKeyDown(KeyCode.Return) && !isPlacing && !isMoving && isRenaming && selectedBuilding)
        {
            RenameBuildingFinish();
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
            CancelBuildingPlace();
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
        if (selectedBuilding != null)
        {
            editText.text = "Selected " + selectedBuilding.displayName;
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
        selectMenuBack.SetActive(true);
        selectMenuTree.SetActive(true);
    }
    void SelectBuildingCoRoutine()
    {
        selectMenu.SetActive(true);
        selectMenuBack.SetActive(true);
        selectMenuBuilding.SetActive(true);
    }
    void SelectReactorCoRoutine()
    {
        selectMenu.SetActive(true);
        selectMenuBack.SetActive(true);
        selectMenuReactor.SetActive(true);
    }
    void SelectSolarPlantCoRoutine()
    {
        selectMenu.SetActive(true);
        selectMenuBack.SetActive(true);
        selectMenuSolarPlant.SetActive(true);
    }
    void SelectCoalPlantCoRoutine()
    {
        selectMenu.SetActive(true);
        selectMenuBack.SetActive(true);
        selectMenuCoalPlant.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
