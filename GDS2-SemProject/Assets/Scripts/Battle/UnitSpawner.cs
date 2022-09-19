using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private int cost;
    [SerializeField] private UnitBase unit;
    [SerializeField] private GameController gc;
    [SerializeField] private BattleNode destination;
    [SerializeField] private BattleNode parent;
    [SerializeField] private float spawnTimer = 1;
    [SerializeField] private float spawnDuration = 1;
    private float spawnSpeed;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        if (!destination.IsEnemy() || parent.IsEnemy() || spawnDuration <=0)
        {
            gc.RegainIncome(cost);
            Destroy(gameObject);
        }
        
        if (spawnTimer <= 0)
        {
            unit.SpawnUnit(parent, destination.transform, true);
            spawnTimer = spawnSpeed;
        }

        spawnDuration -= Time.deltaTime;
        spawnTimer -= Time.deltaTime;
    }

    public void Setup(int c, UnitBase u, BattleNode d, BattleNode p)
    {
        cost = c;
        unit = u;
        destination = d;
        parent = p;
        spawnSpeed = u.GetSpawnSpeed();
        spawnDuration = u.GetDuration();
    }


}
