using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private StatManager statManager;
    public Tile placedTile;
    public string displayName = "Tree";

    public void Start()
    {
        statManager = FindObjectOfType<StatManager>();
        statManager.pollutionModifier -= statManager.reactorPollutionEffect;
        statManager.moralModifier += statManager.treeMoralEffect;
    }
}
