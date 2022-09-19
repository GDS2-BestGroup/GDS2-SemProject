using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPositionRef : MonoBehaviour
{
    private Transform target;
    private UnitBase ub;
    private List<Transform> enemyList = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        ub = GetComponent<UnitBase>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            enemyList.Add(collision.transform);
        }

        if (collision.gameObject.tag == "Node")
        {

        }
    }
}
