using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleNode : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private BattleNode[] neighbourNodes;
    private Transform[] neighbourNodeTransform;
    [SerializeField] private LineController line;

    // Castle Stats
    [SerializeField] private float castleMaxHP = 10;
    [SerializeField] private float castleCurrHP;

    [SerializeField] private bool isEnemy = true;
    [SerializeField] private Unit[] summonedUnits;


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        castleCurrHP = castleMaxHP;
        LineUpdate();

    }

    // Update is called once per frame
    void Update()
    {
        if(castleCurrHP <= 0)
        {
            CastleCapture();
        }

        //castleCurrHP -= 1 * Time.deltaTime;
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

    private void CastleCapture()
    {
        isEnemy = !isEnemy;
        sr.color = isEnemy ? Color.red : Color.blue; 
        castleCurrHP = castleMaxHP;
    }

}
