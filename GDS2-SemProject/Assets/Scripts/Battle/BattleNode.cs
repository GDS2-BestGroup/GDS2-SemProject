using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleNode : MonoBehaviour
{
    [SerializeField] private BattleNode[] neighbourNodes;
    private Transform[] neighbourNodeTransform;
    [SerializeField] private LineController line;
    // Start is called before the first frame update
    void Start()
    {
        LineUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void LineUpdate()
    { 
       neighbourNodeTransform = new Transform[neighbourNodes.Length];
       for (int i = 0; i < neighbourNodes.Length; i++)
       {
              neighbourNodeTransform[i] = neighbourNodes[i].transform;
       }
       line.SetUpLine(neighbourNodeTransform);
    }
}
