using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleNode : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private List<BattleNode> neighbourNodes;

    //[SerializeField] private LineController line;

    // Castle Stats
    [SerializeField] private float castleMaxHP = 10;
    [SerializeField] private float castleCurrHP;

    [SerializeField] private bool isEnemy = true;
    [SerializeField] private Unit[] summonedUnits;

    [SerializeField] private Unit testUnit;

    [SerializeField] private GameObject path;
    //[SerializeField] private GameObject pathParent;

    [SerializeField] private bool isBoss;

    private GameController gc;

    private int splitCount = 1;

    void Awake()
    { 
        sr = GetComponent<SpriteRenderer>();
        castleCurrHP = castleMaxHP;
        CreatePaths();
        foreach(BattleNode i in neighbourNodes)
        {
            i.AddNeighbour(this);
        }

        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void Start()
    {
        if (isEnemy)
        {
            EnemySummonUnits();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If the castle has been 'taken over', switch sides.
        if(castleCurrHP <= 0)
        {
            CastleCapture();
        }

        //castleCurrHP -= 1 * Time.deltaTime;
    }
    
    private void CreatePaths()
    {


        foreach (BattleNode i in neighbourNodes)
        {
            CreatePath(i);
        }
    }

    public void CreatePath(BattleNode i)
    {
        //Variables to create the path at the right length and angle
        float dist;
        Vector3 mid;
        float angle;

        dist = Vector3.Distance(transform.position, i.transform.position);
        mid = (transform.position + i.transform.position) / 2;
        angle = Mathf.Atan2(i.transform.position.y - transform.position.y, i.transform.position.x - transform.position.x) * Mathf.Rad2Deg;

        //Create the path and stretch and rotate it.
        GameObject pp = Instantiate(path, mid, Quaternion.identity, /*pathParent.*/transform);
        pp.GetComponent<PathScript>().SetParents(this, i);
        pp.transform.localScale = new Vector3(dist, 0.7f, 1);
        pp.transform.Rotate(0, 0, angle);
        pp.name = this.name + " to " + i.name;
    }


    private void CastleCapture()
    {
        //Capture the castle and reset its hp
        isEnemy = !isEnemy;
        sr.color = isEnemy ? Color.red : Color.blue; 
        castleCurrHP = castleMaxHP;
        CheckSurround();
        EnemySummonUnits();
        //Let the connected nodes know it has been captured.
        foreach(BattleNode i in neighbourNodes)
        {
            i.CheckSurround();
        }

        //Boss entities can't be surrounded
        /*if (isBoss)
        {
            GameController.EndStage(true);
        }*/
    }

    public void CheckSurround()
    {
        bool surround = false;
        if (neighbourNodes.Count > 1)
        {
            Debug.Log(name + " is checking");
            surround = true;
            int checkSplit = 0;
            foreach (BattleNode i in neighbourNodes)
            {
                if (i.IsEnemy() == isEnemy)
                {
                    surround = false;
                }
                else
                {
                    checkSplit++;
                }
            }
            splitCount = checkSplit;
            splitCount = Mathf.Clamp(splitCount, 1, 5);
        }
        if (surround && !isBoss)
        {
            Debug.Log(this.name + "is Surrounded");
            CastleCapture();
        }else if(splitCount > 0)
        {
            EnemySummonUnits();
        }
    }

    public bool IsEnemy()
    {
        return isEnemy;
    }

    public void AddNeighbour(BattleNode neighbour)
    {
        if (!neighbourNodes.Contains(neighbour))
        {
            neighbourNodes.Add(neighbour);
        }
    }

    private void EnemySummonUnits()
    {
        StopAllCoroutines();
        if(isEnemy){
        foreach(BattleNode i in neighbourNodes)
        {
            if (!i.IsEnemy())
            {
                Debug.Log("S " + this.name);
                StartCoroutine(SummonUnit(testUnit, i.transform));
            }
        }
        }
    }

    private IEnumerator SummonUnit(Unit unit, Transform dest)
    {
        while (isEnemy)
        {
            yield return new WaitForSeconds(unit.GetSpawnSpeed() * (splitCount*0.75f));
            unit.SpawnUnit(transform, dest);
            Debug.Log(name + " " + splitCount);
        }
    }

}
