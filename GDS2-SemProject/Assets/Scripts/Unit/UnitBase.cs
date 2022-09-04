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

    //Range Unit Specific
    public GameObject projectile;
    public Transform firePoint;


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

