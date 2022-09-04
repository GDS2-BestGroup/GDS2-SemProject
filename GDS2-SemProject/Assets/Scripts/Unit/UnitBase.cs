using UnityEngine;

public class UnitBase : MonoBehaviour
{
    public string unitName; //Name of the Unit
    public float health; //Health point
    public float damage; //Damage unit deal each hit
    public float defense; //Damage resistance of the unit
    public float attackSpeed; //Frequency of attack
    public float walkSpeed; //How fast unit move
    public float spawnSpeed; //How fast unit spawn
    public Transform destination;
    [SerializeField] private int cost;
    [SerializeField] private bool isEnemy;

    public BattleNode parent;

    //Range Unit Specific
    public GameObject projectile;
    public Transform firePoint;

    private void Update()
    {
        if(Vector2.Distance(destination.position, transform.position) < 0.5)
        {
            DestroySelf();
        }
    }

    public void SpawnProjectile()
    {
        GameObject bullet = Instantiate(projectile, firePoint.position, Quaternion.identity, gameObject.transform);
        bullet.transform.right = transform.right.normalized;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public float GetSpawnSpeed()
    {
        return spawnSpeed;
    }

    public void SpawnUnit(BattleNode start, Transform end, bool ally)
    {
        UnitBase l = Instantiate(this, start.transform.position, Quaternion.identity);
        l.destination = end;
        l.parent = start;
        if (ally)
        {
            l.tag = "Player";
            l.gameObject.layer = 7;
            isEnemy = false;
            foreach (Transform i in l.transform)
            {
                i.gameObject.layer = 7;
            }
            l.transform.localScale = new Vector3(2, 2, 1);
        }
        else
        {
            l.tag = "Enemy";
            l.gameObject.layer = 6;
            isEnemy = true;
            foreach(Transform i in l.transform)
            {
                i.gameObject.layer = 6;
            }
        }

    }

    public int GetCost()
    {
        return cost;
    }

    public bool IsEnemy()
    {
        return isEnemy;
    }
}

