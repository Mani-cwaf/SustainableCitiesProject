using System.Linq;
using UnityEngine;

public class AnimationsManager : MonoBehaviour
{
    [SerializeField] private Animator pollutionAnimator;
    [SerializeField] private Animator goldAnimator;
    [SerializeField] private Animator goldControllerAnimator;
    [SerializeField] private Animator energyAnimator;
    [SerializeField] private StatManager statManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Sprite houseEnabled;
    [SerializeField] private Sprite houseDisabled;

    void Update()
    {
        if (statManager.pollutionModifier > 0)
        {
            if (statManager.pollutionModifier >= 100)
            {
                pollutionAnimator.SetBool("ShowWarning", false);
                pollutionAnimator.SetBool("ShowWarningBig", true);
            }
            else
            {
                pollutionAnimator.SetBool("ShowWarningBig", false);
                pollutionAnimator.SetBool("ShowWarning", true);
            }
        } 
        else
        {
            pollutionAnimator.SetBool("ShowWarning", false);
            pollutionAnimator.SetBool("ShowWarningBig", false);
        }


        if (gameManager.houses.Any(house => !house.houseEnabled))
        {
            energyAnimator.SetBool("ShowEnergyDepleted", true);
        }
        else
        {
            energyAnimator.SetBool("ShowEnergyDepleted", false);
        }

        pollutionAnimator.speed = Mathf.Clamp(0.05f * Mathf.Log(statManager.pollutionModifier, 1.085f), 0.2f, 4f);
        goldAnimator.speed = Mathf.Clamp(0.02f * Mathf.Log(statManager.Gold, 1.1f), 0.5f, 3f);
        goldControllerAnimator.speed = Mathf.Clamp(0.04f * Mathf.Log(statManager.inCollectionGold, 1.1f), 0.5f, 2f);
        energyAnimator.speed = 1 + Mathf.Clamp(0.1f * Mathf.Log(Mathf.Clamp(statManager.energyBeingUsed - statManager.energyBeingProvided, 1f, 9999999f), 1.1f), 0f, 2.5f);
    }
    public void HouseDisabledAnimation(House house)
    {
        house.gameObject.GetComponent<SpriteRenderer>().sprite = houseDisabled;
    }
    public void HouseEnabledAnimation(House house)
    {
        house.gameObject.GetComponent<SpriteRenderer>().sprite = houseEnabled;
    }
}
