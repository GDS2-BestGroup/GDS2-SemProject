using System.Collections.Generic;
using UnityEngine;

public class EnemyPositionRef : MonoBehaviour
{
    private UnitBase ub;
    [SerializeField] List<Transform> enemyList = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        ub = GetComponentInParent<UnitBase>();
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
            enemyList.Add(collision.transform);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            enemyList.Remove(collision.transform);
        }

        if (collision.gameObject.tag == "Node")
        {
            enemyList.Remove(collision.transform);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (enemyList.Count != 0)
        {
            ub.SetTargetPosition(enemyList[0]);
        }
    }
}
