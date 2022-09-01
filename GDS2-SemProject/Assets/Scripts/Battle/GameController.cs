using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private List<Unit> unitList;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

    public List<Unit> GetUnitList()
    {
        return unitList;
    }

    public void SelectUnit(Unit selected)
    {
        selectedUnit = selected;
    }
}
