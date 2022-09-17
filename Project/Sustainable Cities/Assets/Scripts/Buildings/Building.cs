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
    public float ticksBetweenIncreases = 30;
    public float ticksLeftBetweenIncrease = 30;
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
        ticksBetweenIncreases = Mathf.Clamp(30f + ((0 - statManager.moralModifier) / 10).ConvertTo<int>(), 10, 9999999);
    }
    void BuildingTick()
    {
        if (buildingEnabled)
        {
            ticksLeftBetweenIncrease -= 1;
            if (ticksLeftBetweenIncrease <= 0)
            {
                ticksLeftBetweenIncrease = ticksBetweenIncreases;
                BuildingMoneyTick();
            }
        }
    }

    void BuildingMoneyTick()
    {
        if (statManager.inCollectionGold + goldIncrease <= statManager.maxCollectionGold)
        {
            statManager.inCollectionGold += goldIncrease;
        }
        else
        {
            int difference = statManager.maxCollectionGold - statManager.inCollectionGold;
            if (difference < goldIncrease)
            {
                statManager.inCollectionGold += difference;
            }
        }
    }
}
