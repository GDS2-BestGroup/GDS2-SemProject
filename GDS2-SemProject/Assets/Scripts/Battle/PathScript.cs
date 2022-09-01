using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private BattleNode parent1;
    [SerializeField] private BattleNode parent2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetParents(BattleNode i, BattleNode j)
    {
        parent1 = i;
        parent2 = j;
    }
}
