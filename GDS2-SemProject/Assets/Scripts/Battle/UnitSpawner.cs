using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private int cost;
    [SerializeField] private UnitBase unit;
    [SerializeField] private GameController gg;
    [SerializeField] private BattleNode destination;
    [SerializeField] private BattleNode parent;
    [SerializeField] private float spawnTimer = 1;
    private float spawnSpeed;
    // Start is called before the first frame update
    void Start()
    {
        gg = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        if (!destination.IsEnemy() || parent.IsEnemy())
        {
            gg.RegainIncome(cost);
            Destroy(gameObject);
        }
        
        if (spawnTimer <= 0)
        {
            unit.SpawnUnit(parent, destination.transform, true);
            spawnTimer = spawnSpeed;
        }
        spawnTimer -= Time.deltaTime;
    }

    public void Setup(int c, UnitBase u, BattleNode d, BattleNode p)
    {
        cost = c;
        unit = u;
        destination = d;
        parent = p;
        spawnSpeed = u.GetSpawnSpeed();
    }


}
