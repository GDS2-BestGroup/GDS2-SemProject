using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Text cost;
    [SerializeField] private UnitBase unit;
    private GameController gc;
    private KeyCode numb;

    private Color def;
    private Color sel;
    private ColorBlock norm;

    private GameObject evs;

    private int flip = -1;

    // Start is called before the first frame update

    private KeyCode[] keyCodes =
    {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9
    };

    void Start()
    {
        norm = gameObject.GetComponent<Button>().colors;
        def = norm.normalColor;
        sel = norm.selectedColor;
        evs = GameObject.Find("EventSystem");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(numb))
        {
            SelectUnit();
        }

        if (gc.GetSelectedUnit() == unit)
        {
            norm.normalColor = sel;
            gameObject.GetComponent<Button>().colors = norm;
        }
        else
        {
            norm.normalColor = def;
            gameObject.GetComponent<Button>().colors = norm;
        }

    }

    public void Setup(UnitBase i, GameController gg, int num)
    {
        text.text = i.name;
        unit = i;
        gc = gg;
        cost.text = i.GetCost().ToString();
        numb = keyCodes[num];
    }

    public void SelectUnit()
    {
        flip *= -1;
        if (flip > 0)
        {
            gc.SelectUnit(unit);
        }
        else
        {
            evs.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            gc.SelectUnit(null);
        }
    }
}
