using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour
{
    [SerializeField] private Image img;
    [SerializeField] private Text cost;
    [SerializeField] private Text level;
    [SerializeField] private UnitBase unit;
    private GameController gc;
    private KeyCode numb;

    //private Color def;
    //private Color sel;
    //private ColorBlock norm;

    private GameObject evs;

    //private int flip = -1;

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
/*        norm = gameObject.GetComponent<Button>().colors;
        def = norm.normalColor;
        sel = norm.selectedColor;*/
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
            /*            
            norm.normalColor = sel;
            norm.selectedColor = sel;
            gameObject.GetComponent<Button>().colors = norm;*/
            if (evs.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject != gameObject)
            {
                evs.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(gameObject);
            }
        }
        else
        {
            /*norm.normalColor = def;
            gameObject.GetComponent<Button>().colors = norm;*/
            if (evs.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject != null)
            {
                evs.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            }
        }

    }

    public void Setup(UnitBase i, GameController gg, int num)
    {
        img.sprite = i.GetSprite();
        unit = i;
        gc = gg;
        cost.text = i.GetCost().ToString();
        level.text = gg.GetUnitLevel().ToString();
        numb = keyCodes[num];
    }

    public void SelectUnit()
    {
        if (evs.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject != gameObject)
        {
            evs.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(gameObject);
            gc.SelectUnit(unit);
        }
        else
        {
            evs.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        }
    }
}
