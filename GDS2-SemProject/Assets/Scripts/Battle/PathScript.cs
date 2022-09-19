using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private BattleNode parent1;
    [SerializeField] private BattleNode parent2;

    [SerializeField] private GameController gc;

    [SerializeField] private Color defaultColor;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color deactiveColor;

    private bool mouse = false;
    [SerializeField] private bool active = false;

    private void Awake()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            gameObject.GetComponent<SpriteRenderer>().color = deactiveColor;
        }else
        if (gc.GetSelectedUnit() == null)
        {
            OnMouseExit();
        }

        if (mouse)
        {
            if (Input.GetMouseButtonDown(0) && gc)
            {
                parent1.AddUnits(gc.GetSelectedUnit(), parent2);
                //Debug.Log("Gogo");
            }
        }
    }

    public void SetParents(BattleNode i, BattleNode j)
    {
        parent1 = i;
        parent2 = j;
        if((parent1.IsEnemy() == false || parent2.IsEnemy()==false) && parent1.IsEnemy()!=parent2.IsEnemy())
        {
            SetActive(true);
        }
    }

    public bool IsActive()
    {
        return active;
    }

    public void SetActive(bool set)
    {
        active = set;
    }

    public BattleNode GetParents(int i)
    {
        if(i == 1)
        {
            return parent1;
        }
        else
        {
            return parent2;
        }
    }

    private void OnMouseEnter()
    {
        if (gc.GetSelectedUnit() != null && active)
        {
            //Debug.Log("mouse " + this.name);
            mouse = true;
            gameObject.GetComponent<SpriteRenderer>().color = hoverColor;
        }
    }

    private void OnMouseExit()
    {
        mouse = false;
        gameObject.GetComponent<SpriteRenderer>().color = defaultColor;
    }
}
