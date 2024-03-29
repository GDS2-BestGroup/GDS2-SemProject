using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private GameData gd;
    [SerializeField] private GameObject um;
    [SerializeField] private Text unitLevelText;
    [SerializeField] private Text currentGoldText;
    [SerializeField] private MapCanvas mp;

    private int gold = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (um.activeSelf)
        {
            um.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !SceneManager.GetActiveScene().name.Contains("BattleMap") && !SceneManager.GetActiveScene().name.Contains("MainMenu"))
        {
            if (!um.activeSelf)
            {
                um.SetActive(true);
            }
            else
            {
                um.SetActive(false);
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape) && um.activeSelf)
        {
            um.SetActive(false);
        }

        unitLevelText.text = "Current Unit Level: " + gd.GetUnitLevels();
        currentGoldText.text = "Current Gold: " + gd.GetGold();
    }

    public void BuyUpgrade(int cost)
    {
        gd.UseGold(cost);
        gd.UnitLevelUp();
        mp.UpdateGold();
    }

    public bool Afford(int cost)
    {
        return gd.CheckCost(cost);
    }

    public bool MenuActive()
    {
        return um.activeSelf;
    }

    public void OpenUpgradeScreen()
    {
        um.SetActive(true);
    }

    public void CloseUpgradeScreen()
    {
        um.SetActive(false);
    }
}
