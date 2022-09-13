using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Building : MonoBehaviour
{
    private StatManager statManager;
    public int houseMaxCollectionsGoldIncrease = 5;
    public float timeBetweenIncreases = 2f;
    public float timeLeftBetweenIncreases = 2f;
    public int goldIncrease = 1;
    public Tile placedTile;
    public string displayName = "Building";
    public bool buildingEnabled = true;

    public void Start()
    {
        statManager = FindObjectOfType<StatManager>();
        statManager.energyBeingUsed++;
        statManager.maxCollectionGold += houseMaxCollectionsGoldIncrease;
        InvokeRepeating("BuildingTick", 1, 0.1f);
    }
    private void Update()
    {
        timeBetweenIncreases = Mathf.Clamp(2f + (statManager.pollutionModifier - statManager.moralModifier) / 50, 1, 9999999);
    }
    void BuildingTick()
    {
        timeLeftBetweenIncreases -= 0.1f;
        if (timeLeftBetweenIncreases <= 0)
        {
            timeLeftBetweenIncreases = timeBetweenIncreases;
            BuildingMoneyTick();
        }
    }

    void BuildingMoneyTick()
    {
        if (buildingEnabled && statManager.inCollectionGold + goldIncrease <= statManager.maxCollectionGold)
        {
            statManager.inCollectionGold += goldIncrease;
        }
        else if (buildingEnabled)
        {
            int difference = statManager.maxCollectionGold - statManager.inCollectionGold;
            if (difference < goldIncrease)
            {
                statManager.inCollectionGold += difference;
            }
        }
    }
}
