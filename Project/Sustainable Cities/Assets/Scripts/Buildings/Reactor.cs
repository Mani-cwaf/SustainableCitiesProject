using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactor : MonoBehaviour
{
    private StatManager statManager;
    public Tile placedTile;
    public Tile placedTileX1;
    public Tile placedTileY1;
    public Tile placedTileXY1;
    public string displayName = "Reactor";

    public void Start()
    {
        statManager = FindObjectOfType<StatManager>();
        statManager.energyBeingProvided += statManager.reactorEnergyBeingProvided;
        statManager.pollutionModifier += statManager.reactorPollutionEffect;
        statManager.moralModifier -= statManager.reactorMoralEffect;
    }
}
