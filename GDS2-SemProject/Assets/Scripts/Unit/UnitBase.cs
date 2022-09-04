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
    public Vector2 destination;

    //Range Unit Specific
    public GameObject projectile;
    public Transform firePoint;

    public void Start()
    {

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

}

