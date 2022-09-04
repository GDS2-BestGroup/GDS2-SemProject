using UnityEngine;

public class UnitBase : MonoBehaviour
{
    [SerializeField] private string unitName; //Name of the Unit
    [SerializeField] private float health;//Health point
    [SerializeField] private float damage; //Damage unit deal each hit
    [SerializeField] private float defense;//Damage resistance of the unit
    [SerializeField] private float attackSpeed; //Frequency of attack
    [SerializeField] private float walkSpeed; //How fast unit move
    [SerializeField] private float spawnSpeed;//How fast unit spawn
    public Transform destination;
    [SerializeField] private int cost;

    //Range Unit Specific
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;


    public void SpawnProjectile()
    {
        GameObject bullet = Instantiate(projectile, firePoint.position, Quaternion.identity);
        bullet.transform.right = transform.right.normalized;
        bullet.tag = gameObject.tag;
        bullet.layer = (gameObject.tag == "Player") ? LayerMask.NameToLayer("Player") : LayerMask.NameToLayer("Enemy");
        bullet.GetComponent<ProjectileAttackDealer>().damage = damage;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public float GetSpawnSpeed()
    {
        return spawnSpeed;
    }

    public float GetHealth()
    {
        return health;
    }

    public void SetHealth(float value)
    {
        health = value;
    }
    public float GetDamage()
    {
        return damage;
    }

    public float GetDefense()
    {
        return defense;
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }

    public float GetWalkSpeed()
    {
        return walkSpeed;
    }
     

    public void SpawnUnit(Transform start, Transform end, bool ally)
    {
        UnitBase l = Instantiate(this, start.position, Quaternion.identity);
        l.destination = end;
        if (ally)
        {
            l.tag = "Player";

        }
        else
        {
            l.tag = "Enemy";
        }

    }

    public int GetCost()
    {
        return cost;
    }
}

