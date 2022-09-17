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
    private bool mouse = false;

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
    }

    private void OnMouseEnter()
    {
        if (gc.GetSelectedUnit() != null)
        {
            Debug.Log("mouse " + this.name);
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
