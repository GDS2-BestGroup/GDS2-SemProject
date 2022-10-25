using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    public LevelNode[] regionZeroLvls; //Tutorial Level
    public LevelNode[] regionOneLvls;
    public LevelNode[] regionTwoLvls;
    public LevelNode[] regionThreeLvls;

    public LevelNode[] neighbours;
    public bool paused;
    [SerializeField] public bool[] overworldStatus = { true, false, false, false };
    public bool[] lvlStatusRegionZero = { true, false, false }; //Tutorial Level
    public bool[] lvlStatusRegionOne = { true, false, false, false, false, false, false, false };
    public bool[] lvlStatusRegionTwo = { true, false, false, false, false, false, false, false };
    public bool[] lvlStatusRegionThree = { true, false, false, false, false, false, false, false };

    public int morale;
    [SerializeField] private int baseIncome = 7;
    [SerializeField] private bool disableTut = false;

    public string previousLevel;
    public int currentLevel;
    public int currentRegion;

    [SerializeField] private List<UnitBase> fullUnitList;
    [SerializeField] private List<UnitBase> unitList;
    private int unitSequence = 0;

    private bool additionalIncome = false;
    private bool regionOneComplete = false;
    private bool regionTwoComplete = false;
    private bool regionThreeComplete = false;


    [SerializeField] private int gold;

    [SerializeField] private int unitLevels = 1;
    [SerializeField] private MapCanvas mc;
    private LevelTransition lvlTransition;
    private Button disableBtn;

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
        paused = false;
        lvlTransition = GameObject.Find("LevelTransition").GetComponent<LevelTransition>();
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
        else if (SceneManager.GetActiveScene().name == "Region3")
        {
            regionThreeLvls = FindObjectsOfType<LevelNode>();
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
        else if (region == 3)
        {
            return regionThreeLvls;
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
        else if (region == 3)
        {
            return lvlStatusRegionThree;
        }
        return null;
    }

    public void LoseBattle()
    {
        morale -= 300;
        foreach(UnitBase ub in fullUnitList)
        {
            ub.LevelReset();
        }
        CheckMorale();
        mc.UpdateMorale();
    }

    public void WinBattle()
    {
        morale += 100;
        gold += 50;
        baseIncome += 1;
        foreach (UnitBase ub in fullUnitList)
        {
            ub.LevelReset();
        }
        CheckMorale();
        CheckFinalWin();
        RegionUnlock();
        mc.UpdateMorale();
        mc.UpdateGold();
    }

    public int GetBaseIncome()
    {
        return baseIncome;
    }

    public void CheckMorale()
    {
        if (morale <= 0 && SceneManager.GetActiveScene().name != "LoseScene")
        {
            //SceneManager.LoadScene("LoseScene");
            lvlTransition.FadeToLevel("LoseScene");
        }

        if (morale >= 800 && additionalIncome == false)
        {
            additionalIncome = true;
            baseIncome += 1;
        }
        else if (additionalIncome == true && morale < 800)
        {
            additionalIncome = false;
            baseIncome -= 1;
        }

        if (morale > 1000)
        {
            morale = 1000;
        }
    }

    private void CheckFinalWin()
    {

        if (!regionOneComplete)
        {
            foreach (LevelNode level in regionOneLvls)
            {
                if (level.isFinalLevel && lvlStatusRegionOne[(int)level.levelNum - 1])
                {
                    regionOneComplete = true;
                }
            }
        }

        if (!regionTwoComplete)
        {
            foreach (LevelNode level in regionTwoLvls)
            {
                if (level.isFinalLevel && lvlStatusRegionTwo[(int)level.levelNum - 1])
                {
                    regionTwoComplete = true;
                }
            }
        }

        if (!regionThreeComplete)
        {
            foreach (LevelNode level in regionThreeLvls)
            {
                if (level.isFinalLevel && lvlStatusRegionThree[(int)level.levelNum - 1])
                {
                    regionThreeComplete = true;
                }
            }
        }

        Debug.Log("level one is " + regionOneComplete + " and level two is " + regionTwoComplete);

        if (regionOneComplete && regionTwoComplete && regionThreeComplete)
        {
            //SceneManager.LoadScene("WinScene");
            lvlTransition.FadeToLevel("WinScene");
        }
    }

    public List<UnitBase> GetUnitList()
    {
        return unitList;
    }

    public void RegionUnlock()
    {
        if (!overworldStatus[1])
        {
            foreach (LevelNode level in regionZeroLvls)
            {
                if (level.isFinalLevel && lvlStatusRegionZero[(int)level.levelNum - 1])
                {
                    overworldStatus[1] = true;
                    disableTut = true; //Disables tutorial for when tutorial has been completed
                }
            }
        }

       if (!overworldStatus[2])
       {
            foreach (LevelNode level in regionOneLvls)
            {
                if (level.isFinalLevel && lvlStatusRegionOne[(int)level.levelNum - 1])
                {
                    overworldStatus[2] = true;
                }
            }
       }

        if (!overworldStatus[3])
        {
            foreach (LevelNode level in regionTwoLvls)
            {
                if (level.isFinalLevel && lvlStatusRegionTwo[(int)level.levelNum - 1])
                {
                    overworldStatus[3] = true;
                }
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

    public void DisableTut()
    {
        GameObject[] tutCanvas = GameObject.FindGameObjectsWithTag("Panel");
        if (tutCanvas != null)
        {
            foreach (GameObject t in tutCanvas)
            {
                t.SetActive(false);
            }
            disableTut = true;
            disableBtn.interactable = false;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        lvlTransition = GameObject.Find("LevelTransition").GetComponent<LevelTransition>();
        GameObject disable = GameObject.Find("PauseUI");
        if (disable)
        {
            Debug.Log("Found Pause UI");
            Button[] disableBtns = disable.GetComponentsInChildren<Button>(true);
            foreach (Button b in disableBtns)
            {
                if (b.name == "DisableTutBtn")
                {
                    disableBtn = b;
                    Debug.Log("Button Found");
                }
            }
            disableBtn.onClick.RemoveAllListeners();
            disableBtn.onClick.AddListener(DisableTut);
        }
        if (disableTut)
        {
            DisableTut();
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

    public int GetGold()
    {
        return gold;
    }

    public int GetUnitLevels()
    {
        return unitLevels;
    }

    public void AddGold(int goldToAdd)
    {
        gold += goldToAdd;
        if (gold <= 0)
        {
            gold = 0;
        }
    }

    public void UnitLevelUp()
    {
        unitLevels += 1;
    }
}
