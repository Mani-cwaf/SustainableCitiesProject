using Unity.VisualScripting;
using UnityEngine;

public class House : MonoBehaviour
{
    private StatManager statManager;
    public int houseMaxCollectionsGoldIncrease = 5;
    public float ticksBetweenIncreases = 30;
    public float ticksLeftBetweenIncrease = 30;
    public int goldIncrease = 1;
    public Tile placedTile;
    public string displayName = "House";
    public bool houseEnabled = true;

    public void Start()
    {
        statManager = FindObjectOfType<StatManager>();
        statManager.energyBeingUsed++;
        statManager.maxCollectionGold += houseMaxCollectionsGoldIncrease;
        InvokeRepeating("HouseTick", 1, 0.1f);
    }
    private void Update()
    {
        ticksBetweenIncreases = Mathf.Clamp(30f + ((0 - statManager.moralModifier) / 10).ConvertTo<int>(), 10, 9999999);
    }
    void HouseTick()
    {
        if (houseEnabled)
        {
            ticksLeftBetweenIncrease -= 1;
            if (ticksLeftBetweenIncrease <= 0)
            {
                ticksLeftBetweenIncrease = ticksBetweenIncreases;
                HouseMoneyTick();
            }
        }
    }

    void HouseMoneyTick()
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
