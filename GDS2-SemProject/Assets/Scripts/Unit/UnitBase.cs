using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UnitBase : MonoBehaviour
{
    [SerializeField] private string unitName; //Name of the Unit
    [SerializeField] private float health; //Health point
    [SerializeField] private float damage; //Damage unit deal each hit
    [SerializeField] private float defense; //Damage resistance of the unit
    [SerializeField] private float attackSpeed; //Frequency of attack
    [SerializeField] private float walkSpeed; //How fast unit move
    [SerializeField] private float spawnSpeed; //How fast unit spawn
    [SerializeField] private float spawnDuration; //How long the unit spawns for
    [SerializeField] private Transform destination;
    [SerializeField] private int cost;
    [SerializeField] private bool isEnemy;
    [SerializeField] private Sprite sprite;
    [SerializeField] private UnitSpawner spawner;
    [SerializeField] private List<AudioClip> deathSound = new List<AudioClip>();
    [SerializeField] private List<AudioClip> attackSound = new List<AudioClip>();
    [SerializeField] private AudioSource audioSource;

    private float angle;

    public BattleNode parent;

    //Range Unit Specific
    [SerializeField] GameObject projectile; //Prefab of projectile
    [SerializeField] Transform firePoint;
    private Transform targetPosition; //Used for Bullet Destination

    private GameController gc;
    private void Awake()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    private void Update()
    {
        //Time.timeScale = gc.GetTime(); 
        if(Vector2.Distance(destination.position, transform.position) < 0.5)
        {
            //Debug.Log("Destination Reached");
            DestroySelf();
        }

        if (audioSource)
        {
            Debug.Log("as");
        }
    }

    public void SpawnProjectile()
    {
        GameObject bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);
        if (bullet.TryGetComponent(out ProjectileAttackDealer pad))
        {
            pad.SetDamage(damage);
            if (pad.TryGetComponent(out ProjectileMovement pm))
            {
                pm.setTarget(targetPosition);
            }
        }

        else if (bullet.TryGetComponent(out CurveProjectileMovement cpm))
        {
            cpm.setTarget(targetPosition);
            CurveProjectileAttackDealer cad = cpm.GetComponentInChildren<CurveProjectileAttackDealer>();
            cad.SetDamage(damage);
        }
        bullet.layer = (gameObject.tag == "Player") ? 7 : 6;
    }

    public void DestroySelf()
    {
        if (spawner)
        {
            spawner.RemoveFromList(gameObject);
        }
        Destroy(gameObject);
    }


    public void SpawnUnit(BattleNode start, Transform end, bool enemy, UnitSpawner u)
    {
        UnitBase l = Instantiate(this, start.transform.position, Quaternion.identity);
        spawner = u;
        spawner.AddToList(l.gameObject);
        l.destination = end;
        l.parent = start;
        angle = Mathf.Atan2(end.position.y - start.transform.position.y, end.position.x - start.transform.position.x) * Mathf.Rad2Deg;
        if (Mathf.Abs(angle) > 90)
        {
            angle += 180f;
            l.transform.localScale = new Vector3(-2, 2, 1);
        }
        else
        {
            l.transform.localScale = new Vector3(2, 2, 1);
        }
        l.transform.Rotate(0, 0, angle);

        if (!enemy)
        {
            l.tag = "Player";
            l.gameObject.layer = 7;
            isEnemy = false;
            foreach (Transform i in l.transform)
            {
                i.gameObject.layer = 7;
            }
            
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

    public void SetTargetPosition(Transform tp)
    {
        targetPosition = tp;
    }
    
    public Sprite GetSprite()
    {
        return sprite;
    }

    public float GetDuration()
    {
        return spawnDuration;
    }

    public void PlayDeathSound()
    {
        audioSource.PlayOneShot(deathSound[Random.Range(0, deathSound.Count())], 0.5f);
    }

    public void PlayAttackSound()
    {
        audioSource.PlayOneShot(attackSound[Random.Range(0, deathSound.Count())], 0.5f);
    }
}

