using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleNode : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private List<BattleNode> neighbourNodes;

    //[SerializeField] private LineController line;

    // Castle Stats
    [SerializeField] private float castleMaxHP = 200;
    [SerializeField] private float castleCurrHP;

    [SerializeField] private bool isEnemy = true;
    [SerializeField] private List<UnitBase> enemyUnits;
    [SerializeField] private List<UnitBase> summonedUnits;

    [SerializeField] private UnitSpawner uSpawn;

    //[SerializeField] private Unit testUnit;

    [SerializeField] private GameObject path;
    [SerializeField] private List<GameObject> pathList;

    [SerializeField] private bool isBoss;

    [SerializeField] private GameController gc;

    [SerializeField] private Image healthBar;

    private float flipBuffer;

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
        if (flipBuffer > 0)
        {
            flipBuffer -= 1 * Time.deltaTime;
        }
        //castleCurrHP -= 1 * Time.deltaTime;
    }
    
    private void CreatePaths()
    {
        foreach(GameObject i in pathList)
        {
            Destroy(i);
        }
        pathList.Clear();
        if (isEnemy)
        {
            foreach (BattleNode i in neighbourNodes)
            {
                if (i.IsEnemy())
                {
                    CreatePath(i);
                }
            }
        }
        else
        {
            foreach(BattleNode i in neighbourNodes)
            {
                CreatePath(i);
            }
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
        GameObject pp = Instantiate(path, mid, Quaternion.identity, transform);
        pp.GetComponent<PathScript>().SetParents(this, i);
        pp.transform.localScale = new Vector3(dist, 0.7f, 1);
        pp.transform.Rotate(0, 0, angle);
        pp.name = this.name + " to " + i.name;
        pathList.Add(pp);
    }

    public void TakeDamage(float damage)
    {
        if (flipBuffer <= 0)
        {
            flipBuffer = 0.05f;
            castleCurrHP -= damage;

            //If the castle has been 'taken over', switch sides.
            if (castleCurrHP <= 0)
            {
                castleCurrHP = castleMaxHP;
                CastleCapture();
            }

            UpdateHealth();
        }
    }
    private void CastleCapture()
    {
            //Capture the castle and reset its hp
            if (isBoss)
            {
                gc.EndGame(isEnemy);
            }
            isEnemy = !isEnemy;
            sr.color = isEnemy ? Color.red : Color.blue;
            summonedUnits.Clear();
            CheckSurround();
            StopAllCoroutines();
            if (isEnemy)
            {
                gameObject.layer = LayerMask.NameToLayer("Enemy");
                EnemySummonUnits();
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer("Player");
                CreatePaths();
            }
            //Let the connected nodes know it has been captured.
            foreach (BattleNode i in neighbourNodes)
            {
                i.CheckSurround();
            }
            castleCurrHP = castleMaxHP;
            UpdateHealth();
    }

    public void CheckSurround()
    {
        if (!isEnemy)
        {
            CreatePaths();
        }
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
        }else if(splitCount > 0 && isEnemy)
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
        foreach(BattleNode i in neighbourNodes)
        {
            if (!i.IsEnemy())
            {
                foreach (UnitBase e in enemyUnits)
                {
                    //Debug.Log("S " + this.name);
                    StartCoroutine(EnemySummonUnit(e, i.transform));
                }
            }
        }
    }

    private IEnumerator EnemySummonUnit(UnitBase unit, Transform dest)
    {
        while (isEnemy)
        {
            yield return new WaitForSeconds(unit.GetSpawnSpeed() * (splitCount*0.75f));
            unit.SpawnUnit(this, dest, false);
            //Debug.Log(name + " " + splitCount);
        }
    }

/*    private IEnumerator AllySummonUnit(UnitBase unit, BattleNode dest)
    {
        while (dest.IsEnemy())
        {
            yield return new WaitForSeconds(unit.GetSpawnSpeed());
            unit.SpawnUnit(this, dest.transform, true);
            Debug.Log("Summoned");
        }
    }*/

    public void AddUnits(UnitBase i, BattleNode dest)
    {
        //summonedUnits.Add(i);
        if (!isEnemy && dest.IsEnemy())
        {
            if (gc.AffordCost(i.GetCost()))
            {
                gc.UseIncome(i.GetCost());
                UnitSpawner us = Instantiate(uSpawn, transform.position, Quaternion.identity, transform);
                us.Setup(i.GetCost(), i, dest, this);
                //StartCoroutine(AllySummonUnit(i, dest));
            }
        }
    }

    private void UpdateHealth()
    {
        healthBar.fillAmount = castleCurrHP / castleMaxHP;
    }
}
