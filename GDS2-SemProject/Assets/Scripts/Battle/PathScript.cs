using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private BattleNode parent1;
    [SerializeField] private BattleNode parent2;

    private GameController gc;

    private bool mouse = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (mouse)
        {
            if (Input.GetMouseButtonDown(0))
            {
                parent1.AddUnits(gc.GetSelectedUnit(), parent2);
            }
        }
    }

    public void SetParents(BattleNode i, BattleNode j, GameController g)
    {
        parent1 = i;
        parent2 = j;
        gc = g;
    }

    private void OnMouseOver()
    {
        mouse = true;
    }

    private void OnMouseExit()
    {
        mouse = false;
    }
}
