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

    private float timeSpeed;

    private void Awake()
    {
        
    }

    void Start()
    {
        currIncome = gameIncome;
        //gd = GameObject.Find("GameData").GetComponent<GameData>();
        if (gd)
        {
            gameIncome = gd.GetBaseIncome();
        }
        currIncome = gameIncome;
    }

    // Update is called once per frame
    void Update()
    {
        incomeText.text = currIncome.ToString();
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
                gd.GetLevelCompletion(gd.currentRegion)[gd.currentLevel] = true;
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
        }
        else
        {
            timeSpeed = 1;
            speedText.text = "x1";
        }
    }

    public float GetTime()
    {
        return timeSpeed;
    }
}
