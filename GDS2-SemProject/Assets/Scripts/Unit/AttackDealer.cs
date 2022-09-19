using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDealer : MonoBehaviour
{
    private UnitBase unitStats;
    private float damage;

    // Start is called before the first frame update
    void Start()
    {
        unitStats = GetComponentInParent<UnitBase>();
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
            float damageToDeal = DealDamage(unitStats.GetDamage(), target.GetDefense());
            target.TakeDamage(damageToDeal);
        }

        if(collision.gameObject.tag == "Node")
        {
            BattleNode target = collision.gameObject.GetComponent<BattleNode>();
            float damageToDeal = DealDamage(unitStats.GetDamage(), 0);
            target.TakeDamage(damageToDeal);
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
