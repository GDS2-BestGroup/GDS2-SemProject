using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitShop : MonoBehaviour
{
    [SerializeField] private List<UnitBase> units;
    private GameController gc;
    [SerializeField] private GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        units = gc.GetUnitList();
        MakeUnitButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MakeUnitButtons()
    {
        for(int i = 0; i < units.Count; i++)
        {
            UnitButton bb = Instantiate(button, transform).GetComponent<UnitButton>();
            bb.Setup(units[i], gc, i);
        }
    }

}
