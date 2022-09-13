using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarPlant : MonoBehaviour
{
    private StatManager statManager;
    public Tile placedTile;
    public string displayName = "Solar Plant";

    public void Start()
    {
        statManager = FindObjectOfType<StatManager>();
        statManager.energyBeingProvided += statManager.solarPlantEnergyBeingProvided;
        statManager.moralModifier += statManager.solarPlantMoralEffect;
    }
}
