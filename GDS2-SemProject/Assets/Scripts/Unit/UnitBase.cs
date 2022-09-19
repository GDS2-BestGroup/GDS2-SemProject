using UnityEngine;

public class UnitBase : MonoBehaviour
{
    [SerializeField] private string unitName; //Name of the Unit
    [SerializeField] private float health; //Health point
    [SerializeField] private float damage; //Damage unit deal each hit
    [SerializeField] private float defense; //Damage resistance of the unit
    [SerializeField] private float attackSpeed; //Frequency of attack
    [SerializeField] private float walkSpeed; //How fast unit move
    [SerializeField] private float spawnSpeed; //How fast unit spawn
    [SerializeField] private Transform destination;
    [SerializeField] private int cost;
    [SerializeField] private bool isEnemy;

    public BattleNode parent;

    //Range Unit Specific
    [SerializeField] GameObject projectile; //Prefab of projectile
    [SerializeField] Transform firePoint;
    private Vector2 targetPosition; //Used for Curve Bullet

    private void Update()
    {
        if(Vector2.Distance(destination.position, transform.position) < 0.5)
        {
            DestroySelf();
        }
    }

    public void SpawnProjectile()
    {
        GameObject bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);
        if (bullet.TryGetComponent(out ProjectileAttackDealer pad))
        {
            bullet.GetComponent<ProjectileAttackDealer>().SetDamage(damage);
        }

        else if (bullet.TryGetComponent(out AttackDealer ad))
        {
            bullet.GetComponent<AttackDealer>().SetDamage(damage);
        }

        bullet.layer = (gameObject.tag == "Player") ? 7 : 6;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
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
            l.transform.localScale = new Vector3(-2, 2, 1);
        }

    }

    public bool IsEnemy()
    {
        return isEnemy;
    }

    public int GetCost()
    {
        return cost;
    }

    public float GetSpawnSpeed()
    {
        return spawnSpeed;
    }


    public float GetDamage()
    {
        return damage;
    }

    public float GetDefense()
    {
        return defense;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public float GetWalkSpeed()
    {
        return walkSpeed;
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }

    public Transform GetDest()
    {
        return destination;
    }

    public float GetHealth()
    {
        return health;
    }

    public void SetTargetPosition(Vector2 tp)
    {
        targetPosition = tp;
    }
}

