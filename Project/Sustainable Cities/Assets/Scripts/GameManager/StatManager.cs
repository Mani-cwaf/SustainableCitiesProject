using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public int Gold = 150;
    public int maxCollectionGold = 50;
    public int inCollectionGold = 0;
    public int treeCost = 10;
    public int buildingCost = 25;
    public int solarPlantCost = 35;
    public int coalPlantCost = 20;
    public int reactorCost = 105;
    public int treeCount;
    public int buildingCount;
    public int solarPlantCount;
    public int coalPlantCount;
    public int reactorCount;
    public int energyBeingUsed;
    public int buildingEnergyBeingUsed = 1;
    public int energyBeingProvided;
    public int coalPlantEnergyBeingProvided = 2;
    public int solarPlantEnergyBeingProvided = 3;
    public int reactorEnergyBeingProvided = 10;
    public float moralModifier;
    public float pollutionModifier;
    public float coalPlantPollutionEffect = 15;
    public float reactorPollutionEffect = 5;
    public float treeMoralEffect = 15;
    public float reactorMoralEffect = 5;
    public float solarPlantMoralEffect = 5;
    public float coalPlantMoralEffect = 10;
}