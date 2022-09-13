using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalPlant : MonoBehaviour
{
    private StatManager statManager;
    public Tile placedTile;
    public string displayName = "Coal Plant";

    public void Start()
    {
        statManager = FindObjectOfType<StatManager>();
        statManager.energyBeingProvided += statManager.coalPlantEnergyBeingProvided;
        statManager.pollutionModifier += statManager.coalPlantPollutionEffect;
        statManager.moralModifier -= statManager.coalPlantMoralEffect;
    }
}
