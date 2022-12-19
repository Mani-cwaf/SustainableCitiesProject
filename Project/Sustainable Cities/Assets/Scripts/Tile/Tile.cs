using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isOccupied;
    public Tree currentTree;
    public House currentHouse;
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
            buttons.houseInMovement = "tree";
        }
        if (currentHouse != null)
        {
            buttons.SelectHouse(currentHouse);
            buttons.houseInMovement = "house";
        }
        if (currentReactor != null)
        {
            buttons.houseInMovement = "reactor";
            buttons.SelectReactor(currentReactor);
        }
        if (currentSolarPlant != null)
        {
            buttons.houseInMovement = "solar";
            buttons.SelectSolarPlant(currentSolarPlant);
        }
        if (currentCoalPlant != null)
        {
            buttons.houseInMovement = "coal";
            buttons.SelectCoalPlant(currentCoalPlant);
        }
    }
}