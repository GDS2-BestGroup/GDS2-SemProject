using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUnit : MonoBehaviour
{
    [SerializeField] private UpgradeManager um;
    [SerializeField] private UpgradeUnit previousUpgrade;
    [SerializeField] private UpgradeUnit nextUpgrade;
    [SerializeField] private Button unitButton;
    [SerializeField] private Text unitText;
    [SerializeField] private Text unitCostText;
    [SerializeField] private bool locked = true;
    //[SerializeField] private bool isBuff = false; //If the upgrade is a level up or a unit specific buff
    [SerializeField] private bool repeatable = false;
    [SerializeField] private int upgradeLevel = 0;
    [SerializeField] private int maxUpLevel = 4;
    [SerializeField] private int cost;
    [SerializeField] private string text;

    // Start is called before the first frame update
    void Start()
    {
        if (locked)
        {
            Lock();
        }
        um = GameObject.Find("Managers").GetComponent<UpgradeManager>();
        unitText.text = text;
        unitCostText.text = "Unit cost is: " + cost.ToString() + " gold";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyUpgrade()
    {
        if (um.Afford(cost) && upgradeLevel < maxUpLevel)
        {
            um.BuyUpgrade(cost);
            if (!repeatable)
            {
                Lock();
                nextUpgrade.Unlock();
            }
            else
            {
                upgradeLevel += 1;
                cost += 50 * upgradeLevel;
                unitCostText.text = "Unit cost is: " + cost.ToString() + " gold";
            }
        }
    }

    public int GetCost()
    {
        return cost;
    }

    public void SetCost(int i)
    {
        cost = i;
    }

    public void Lock()
    {
        locked = true;
        unitButton.interactable = false;
    }

    public void Unlock()
    {
        locked = false;
        unitButton.interactable = true;
    }
}
