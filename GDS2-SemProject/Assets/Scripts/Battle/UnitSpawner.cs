using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private int cost = 0;
    [SerializeField] private UnitBase unit;
    [SerializeField] private GameController gc;
    [SerializeField] private BattleNode destination;
    [SerializeField] private BattleNode parent;
    [SerializeField] private float spawnTimer = 1;
    [SerializeField] private float spawnDuration = 1;
    [SerializeField] private bool isEnemy = true;
    private float spawnSpeed;

    [SerializeField] private List<GameObject> childUnits;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        if (!isEnemy)
        {
            if (spawnDuration <= 0)
            {
                DestroySequence();
            }

            if (parent.IsEnemy() || !destination.IsEnemy())
            {
                foreach(GameObject u in childUnits)
                {
                    Destroy(u);
                }
                childUnits.Clear();
                DestroySequence();
            }
        }
        else
        {
            if (!parent.IsEnemy() || destination.IsEnemy())
            {
                foreach (GameObject u in childUnits)
                {
                    if (u)
                    {
                        Destroy(u);
                    }
                }
                DestroySequence();
            }
        }
        
        if (spawnTimer <= 0)
        {
            unit.SpawnUnit(parent, destination.transform, isEnemy, this);
            spawnTimer = spawnSpeed;
        }

        spawnDuration -= Time.deltaTime;
        spawnTimer -= Time.deltaTime;
    }

    public void Setup(int c, UnitBase u, float speed, BattleNode d, BattleNode p, bool ie)
    {
        cost = c;
        unit = u;
        destination = d;
        parent = p;
        spawnSpeed = speed;
        spawnDuration = u.GetDuration();
        isEnemy = ie;
    }

    private void DestroySequence()
    {
        gc.RegainIncome(cost);
        Destroy(gameObject);
    }

    public void AddToList(GameObject u)
    {
        childUnits.Add(u);
    }

    public void RemoveFromList(GameObject u)
    {
        childUnits.Remove(u);
    }
}
