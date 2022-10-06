using UnityEngine;

public class CurveProjectileAttackDealer : MonoBehaviour
{
    private float damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ProjectileMovement projectile))
        {
            return;
        }

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            UnitBase target = collision.gameObject.GetComponent<UnitBase>();
            float damageToDeal = DealDamage(damage, target.GetDefense());
            target.TakeDamage(damageToDeal);
            //Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Node")
        {
            BattleNode target = collision.gameObject.GetComponent<BattleNode>();
            float damageToDeal = DealDamage(damage, 0);
            target.TakeDamage(damageToDeal);
            //Destroy(gameObject);
        }
    }

    public float DealDamage(float damage, float defense)
    {
        float damageToDeal = (damage - defense);
        if (damageToDeal <= 0)
        {
            damageToDeal = 1;
        }

        return damageToDeal;
    }

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
}