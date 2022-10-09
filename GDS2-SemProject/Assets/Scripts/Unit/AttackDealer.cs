using UnityEngine;

public class AttackDealer : MonoBehaviour
{
    private UnitBase unitStats;
    private string counter;
    private float damageAddtionPercent;
    private float damage;

    // Start is called before the first frame update
    void Start()
    {
        unitStats = GetComponentInParent<UnitBase>();
        damage = unitStats.GetDamage();
        counter = unitStats.GetCounter();
        damageAddtionPercent = unitStats.GetDamageAddtionPercent();
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
            float damageToDeal = DealDamage(target.GetDefense(), target.GetUnitName());
            target.TakeDamage(damageToDeal);
        }

        if(collision.gameObject.tag == "Node")
        {
            BattleNode target = collision.gameObject.GetComponent<BattleNode>();
            float damageToDeal = DealDamage(0, "Node");
            target.TakeDamage(damageToDeal);
        }
    }

    public float DealDamage(float defense, string c)
    {
        if (c.Equals(counter, System.StringComparison.OrdinalIgnoreCase))
        {
            float damageToDeal = ((damage + damage * damageAddtionPercent) - defense);
            if (damageToDeal <= 0)
            {
                damageToDeal = 1;
            }
            return damageToDeal;
        }

        else if (!c.Equals(counter, System.StringComparison.OrdinalIgnoreCase))
        {
            float damageToDeal = (damage - defense);
            if (damageToDeal <= 0)
            {
                damageToDeal = 1;
            }
            return damageToDeal;
        }

        return default;
    }

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
}
