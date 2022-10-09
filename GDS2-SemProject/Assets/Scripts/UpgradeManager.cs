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
        unitLevelText.text = "Current Unit Level: " + gd.GetUnitLevels();
        currentGoldText.text = "Current Gold: " + gd.GetGold();
    }

    public void BuyUpgrade(int cost)
    {
        gd.UseGold(cost);
        gd.UnitLevelUp();
    }

    public bool Afford(int cost)
    {
        return gd.CheckCost(cost);
    }

    public void CloseUpgradeScreen()
    {
        um.SetActive(false);
    }
}
