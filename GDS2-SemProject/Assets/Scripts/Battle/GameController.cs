using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private UnitBase selectedUnit;
    [SerializeField] private List<UnitBase> unitList;

    [SerializeField] private int baseIncome = 10;
    private int gameIncome;
    private int currIncome;

    [SerializeField] private Text incomeText;

    private void Awake()
    {
        gameIncome = baseIncome;
    }

    void Start()
    {
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
}
