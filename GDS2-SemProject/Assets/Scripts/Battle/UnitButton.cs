using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private UnitBase unit;
    private GameController gc;
    // Start is called before the first frame update


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(UnitBase i, GameController gg)
    {
        text.text = i.name;
        unit = i;
        gc = gg;
    }

    public void SelectUnit()
    {
        gc.SelectUnit(unit);
    }
}
