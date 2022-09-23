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
    public bool[] lvlStatusRegionZero = { true, false, false }; //Tutorial Level
    public bool[] lvlStatusRegionOne = { true, false };
    public bool[] lvlStatusRegionTwo = { true, false };
    public int morale;
    [SerializeField] private int baseIncome = 7;

    public string previousLevel;
    public int currentLevel;
    public int currentRegion;

    [SerializeField] private List<UnitBase> unitList;

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
        morale = 800;
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

        CheckFinalWin();
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
        baseIncome += 2;
        CheckMorale();
        CheckFinalWin();
    }

    public int GetBaseIncome()
    {
        return baseIncome;
    }

    private void CheckMorale()
    {
        if(morale <= 0)
        {
            SceneManager.LoadScene("LoseScene");
        }

        if(morale > 800)
        {
            baseIncome = 8;
        }
    }

    private void CheckFinalWin()
    {
        bool win = true;
        foreach (bool i in lvlStatusRegionOne)
        {
            if (i)
            {
                win = false;
            }
        }
        if (win)
        {
            foreach (bool i in lvlStatusRegionTwo)
            {
                if (i)
                {
                    win = false;
                }
            }
        }
        if (win)
        {
            foreach (bool i in lvlStatusRegionZero)
            {
                if (i)
                {
                    win = false;
                }
            }
        }
        if (win)
        {
            SceneManager.LoadScene("WinScene");
        }
    }

    public List<UnitBase> GetUnitList()
    {
        return unitList;
    }
}
