using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public bool isOccupied;
    public Tree currentTree;
    public Building currentBuilding;
    public Reactor currentReactor;
    public CoalPlant currentCoalPlant;
    public SolarPlant currentSolarPlant;
    private Buttons buttons;

    private void Start()
    {
        buttons = FindObjectOfType<Buttons>();
    }

    private void OnMouseDown()
    {
        if (currentTree != null)
        {
            buttons.SelectTree(currentTree);
            buttons.buildingInMovement = "tree";
        }
        if (currentBuilding != null)
        {
            buttons.SelectBuilding(currentBuilding);
            buttons.buildingInMovement = "building";
        }
        if (currentReactor != null)
        {
            buttons.buildingInMovement = "reactor";
            buttons.SelectReactor(currentReactor);
        }
        if (currentSolarPlant != null)
        {
            buttons.buildingInMovement = "solar";
            buttons.SelectSolarPlant(currentSolarPlant);
        }
        if (currentCoalPlant != null)
        {
            buttons.buildingInMovement = "coal";
            buttons.SelectCoalPlant(currentCoalPlant);
        }
    }
}