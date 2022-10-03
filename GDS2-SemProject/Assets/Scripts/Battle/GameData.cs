using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public LevelNode[] regionZeroLvls; //Tutorial Level
    public LevelNode[] regionOneLvls;
    public LevelNode[] regionTwoLvls;
    public LevelNode[] neighbours;
    [SerializeField] public bool[] overworldStatus = { true, false, false };
    public bool[] lvlStatusRegionZero = { true, false, false }; //Tutorial Level
    public bool[] lvlStatusRegionOne = { true, false, false, false, false, false, false, false };
    public bool[] lvlStatusRegionTwo = { true, false, false, false, false, false, false, false };
    public int morale;
    [SerializeField] private int baseIncome = 7;

    public string previousLevel;
    public int currentLevel;
    public int currentRegion;

    [SerializeField] private List<UnitBase> fullUnitList;
    [SerializeField] private List<UnitBase> unitList;
    private int unitSequence = 0;

    private bool additionalIncome = false;
    private bool regionOneComplete = false;
    private bool regionTwoComplete = false;

    [SerializeField] private int gold;

    private int unitLevels = 1;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameController");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        morale = 600;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Region1")
        {
            regionOneLvls = FindObjectsOfType<LevelNode>();
        }
        else if (SceneManager.GetActiveScene().name == "Region2")
        {
            regionTwoLvls = FindObjectsOfType<LevelNode>();
        }
        else if (SceneManager.GetActiveScene().name == "Region0")
        {
            regionZeroLvls = FindObjectsOfType<LevelNode>();
        }

        if (morale > 1000)
        {
            morale = 1000;
        }

        //CheckFinalWin();
        CheckMorale();
    }

    public LevelNode[] GetLevels(int region)
    {
        if (region == 1)
        {
            return regionOneLvls;
        }
        else if (region == 2)
        {
            return regionTwoLvls;
        }
        else if (region == 0)
        {
            return regionZeroLvls;
        }
        return null;
    }

    public bool[] GetLevelCompletion(int region)
    {
        if (region == 1)
        {
            return lvlStatusRegionOne;
        }
        else if (region == 2)
        {
            return lvlStatusRegionTwo;
        }
        else if (region == 0)
        {
            return lvlStatusRegionZero;
        }
        return null;
    }

    public void LoseBattle()
    {
        morale -= 300;
        CheckMorale();
    }

    public void WinBattle()
    {
        morale += 100;
        baseIncome += 1;
        CheckMorale();
        CheckFinalWin();
        RegionUnlock();
    }

    public int GetBaseIncome()
    {
        return baseIncome;
    }

    public void CheckMorale()
    {
        if(morale <= 0 && SceneManager.GetActiveScene().name != "LoseScene")
        {
            SceneManager.LoadScene("LoseScene");
        }

        if(morale >= 800 && additionalIncome == false)
        {
            additionalIncome = true;
            baseIncome += 1;
        }
        else if(additionalIncome == true && morale < 800)
        {
            additionalIncome = false;
            baseIncome -= 1;
        }

        if(morale > 1000)
        {
            morale = 1000;
        }
    }

    //private void CheckFinalWin()
    //{
    //    bool win = true;
    //    foreach (bool i in lvlStatusRegionZero)
    //    {
    //        if (i)
    //        {
    //            win = false;
    //        }
    //    }
    //    /*if (win)
    //    {
    //        overworldStatus[1] = true;
    //    }*/
  
    //    if (win)
    //    {
    //        foreach (bool i in lvlStatusRegionOne)
    //        {
    //            if (i)
    //            {
    //                win = false;
    //            }
    //        }
    //        /*if (win)
    //        {
    //            overworldStatus[2] = true;
    //        }*/
    //    }

    //    if (win)
    //    {
    //        foreach (bool i in lvlStatusRegionTwo)
    //        {
    //            if (i)
    //            {
    //                win = false;
    //            }
    //        }
    //    }

    //    if (win)
    //    {
    //        SceneManager.LoadScene("WinScene");
    //    }
    //}

    private void CheckFinalWin()
    {

        foreach (LevelNode level in regionOneLvls)
        {
            if (level.isFinalLevel && lvlStatusRegionOne[(int)level.levelNum - 1])
            {
                regionOneComplete = true;
            }
        }


        foreach (LevelNode level in regionTwoLvls)
        {
            if (level.isFinalLevel && lvlStatusRegionTwo[(int)level.levelNum - 1])
            {
                regionTwoComplete = true;
            }
        }

        Debug.Log("level one is " + regionOneComplete + " and level two is " + regionTwoComplete);

        if (regionOneComplete && regionTwoComplete)
        {
            SceneManager.LoadScene("WinScene");
        }
    }

    public List<UnitBase> GetUnitList()
    {
        return unitList;
    }

    public void RegionUnlock()
    {
        foreach(LevelNode level in regionZeroLvls)
        {
            if (level.isFinalLevel && lvlStatusRegionZero[(int)level.levelNum - 1])
            {
                overworldStatus[1] = true;
                //overworldStatus[2] = true;
            }
        }

        foreach (LevelNode level in regionOneLvls)
        {
            if (level.isFinalLevel && lvlStatusRegionOne[(int)level.levelNum - 1])
            {
                overworldStatus[2] = true;
            }
        }
    }

    public void UnlockNextUnit()
    {
        unitSequence += 1;
        if (unitSequence < fullUnitList.Count)
        {
            unitList.Add(fullUnitList[unitSequence]);
        }
    }

    public bool CheckCost(int cost)
    {
        return cost <= gold;
    }

    public void UseGold(int cost)
    {
        gold -= cost;
    }

    public int GetUnitLevels()
    {
        return unitLevels;
    }

    public void UnitLevelUp()
    {
        unitLevels += 1;
    }
}
