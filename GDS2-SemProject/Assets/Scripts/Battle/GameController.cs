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

    [SerializeField] private int gameIncome;
    private int currIncome;

    [SerializeField] private GameData gd;

    [SerializeField] private Text incomeText;
    [SerializeField] private Text speedText;

    private float timeSpeed = 0;

    private void Awake()
    {
        
    }

    void Start()
    {
        Time.timeScale = 0;
        currIncome = gameIncome;
        if (GameObject.Find("Managers").GetComponent<GameData>()) {
            gd = GameObject.Find("Managers").GetComponent<GameData>();
        }
        if (gd)
        {
            gameIncome = gd.GetBaseIncome();
            unitList = gd.GetUnitList();
        }
        currIncome = gameIncome;
        
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
                gd.WinBattle();
                gd.GetLevelCompletion(gd.currentRegion)[gd.currentLevel - 1] = false;
                if (gd.currentLevel < gd.GetLevelCompletion(gd.currentRegion).Length - 1)
                {
                    gd.GetLevelCompletion(gd.currentRegion)[gd.currentLevel] = true;
                }
            }
            else
            {
                gd.LoseBattle();
            }
            SceneManager.LoadScene(gd.previousLevel);
        }
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
}
