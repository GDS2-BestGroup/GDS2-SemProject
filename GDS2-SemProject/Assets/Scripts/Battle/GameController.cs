using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private UnitBase selectedUnit;
    [SerializeField] private List<UnitBase> unitList;

    [SerializeField] private int unitLevels = 1;

    [SerializeField] private int gameIncome;
    private int currIncome;

    [SerializeField] private GameData gd;

    [SerializeField] private Text incomeText;
    [SerializeField] private Text speedText;

    [SerializeField] private Canvas afterScreen;
    [SerializeField] private Text endingText;
    private LevelTransition lvlTransition;

    private float timeSpeed = 0;

    [SerializeField] private bool win = false;
    

    private void Awake()
    {
        Time.timeScale = 0;
        currIncome = gameIncome;
        if (GameObject.Find("Managers"))
        {
            gd = GameObject.Find("Managers").GetComponent<GameData>();
        }
        if (gd)
        {
            gameIncome = gd.GetBaseIncome();
            unitList = gd.GetUnitList();
            unitLevels = gd.GetUnitLevels();
        }
        currIncome = gameIncome;
        
    }

    void Start()
    {
        lvlTransition = GameObject.Find("LevelTransition").GetComponent<LevelTransition>();
    }

    // Update is called once per frame
    void Update()
    {
        incomeText.text = currIncome.ToString();
        /*        if (Input.GetMouseButtonDown(0))
                {
                    selectedUnit = null;
                }*/

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeSpeed();
        }
    }

    public UnitBase GetSelectedUnit()
    {
        return selectedUnit;
    }

    public List<UnitBase> GetUnitList()
    {
        return unitList;
    }

    public void SelectUnit(UnitBase selected)
    {
        selectedUnit = selected;
    }

    public void IncreaseIncome(int value)
    {
        gameIncome += value;
        currIncome += value;
    }

    public void DecreaseIncome(int value)
    {
        gameIncome -= value;
        currIncome -= value;
    }

    public void UseIncome(int value)
    {
        if (AffordCost(value))
        {
            currIncome -= value;
        }
    }

    public void RegainIncome(int value)
    {
        Mathf.Clamp(currIncome += value, 0, gameIncome);
    }

    public bool AffordCost(int value)
    {
        return (currIncome - value) >= 0;
    }

    public void EndGame(bool winner)
    {
        if (gd)
        {
            if (winner)
            {
                win = true;
                gd.WinBattle();
                gd.GetLevelCompletion(gd.currentRegion)[gd.currentLevel - 1] = false;
                /*if (gd.currentLevel < gd.GetLevelCompletion(gd.currentRegion).Length - 1)
                {
                    gd.GetLevelCompletion(gd.currentRegion)[gd.currentLevel] = true;
                }*/
               //LevelNode currentlvl = gd.GetLevels(gd.currentRegion)[gd.currentLevel - 1];
               // Debug.Log("Neighbours length of level " + currentlvl.name + " is " + currentlvl.GetNeighbours().Length);
                if (gd.neighbours.Length > 0)
                {
                    //foreach (bool status in gd.GetLevelCompletion(gd.currentRegion))
                    //{
                    //    if 
                    //}
                    //Debug.Log("Found level " + currentlvl.name);
                    foreach (LevelNode level in gd.neighbours)
                    {
                        gd.GetLevelCompletion(gd.currentRegion)[(int)level.levelNum-1] = true;
                        //Debug.Log("Unlocked level " + level.name);
                    }
                }
                endingText.text = "Victory!";
            }
            else
            {
                gd.LoseBattle();
                endingText.text = "Defeat...";
            }
            Time.timeScale = 0; //Pausing game after battle is done
            afterScreen.gameObject.SetActive(true);
            
        }
    }

    public void ToOverworld()
    {
        //SceneManager.LoadScene(gd.previousLevel);
        lvlTransition.FadeToLevel(gd.previousLevel);
        Time.timeScale = 1; //Undoing the pause set during EndGame
    }

    public void ChangeSpeed()
    {
        if(timeSpeed == 1)
        {
            timeSpeed = 2;
            speedText.text = "x2";
            Time.timeScale = 2;
        }
        else
        {
            timeSpeed = 1;
            speedText.text = "x1";
            Time.timeScale = 1;
        }
    }

    public float GetTime()
    {
        return timeSpeed;
    }

    public void UnlockNextUnit()
    {
        if (win)
        {
            gd.IncreaseBaseIncome();
            gd.IncreaseBaseIncome();
            gd.UnlockNextUnit();
        }
    }

    public int GetUnitLevel()
    {
        unitLevels = gd ? gd.GetUnitLevels() : unitLevels;
        return unitLevels;
    }
}
