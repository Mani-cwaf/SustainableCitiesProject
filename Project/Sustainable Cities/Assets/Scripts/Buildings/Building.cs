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
    public int goldIncrease = 1;
    public Tile placedTile;
    public string displayName = "Building";
    public bool buildingEnabled = true;

    public void Start()
    {
        statManager = FindObjectOfType<StatManager>();
        statManager.energyBeingUsed++;
        statManager.maxCollectionGold += houseMaxCollectionsGoldIncrease;
        InvokeRepeating("BuildingTick", 1, timeBetweenIncreases);
    }
    private void Update()
    {
        timeBetweenIncreases = (2f + Mathf.Clamp(Mathf.Log(Mathf.Clamp(statManager.pollutionModifier, 0, 99999999), 1.06f) * 0.04f, 1f, 12f)) / (1 + (statManager.moralModifier / 100));
    }
    void BuildingTick()
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
