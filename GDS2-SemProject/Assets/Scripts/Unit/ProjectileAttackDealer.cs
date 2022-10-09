using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttackDealer : MonoBehaviour
{
    private List<UnitBase> targetList = new List<UnitBase>();
    private float damage;
    private string counter;
    private float damageAddtionPercent;


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
            targetList.Add(collision.gameObject.GetComponent<UnitBase>());
            UnitBase target = targetList[0];
            float damageToDeal = DealDamage(target.GetDefense(), target.GetUnitName());
            target.TakeDamage(damageToDeal);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Node")
        {
            BattleNode target = collision.gameObject.GetComponent<BattleNode>();
            float damageToDeal = DealDamage(0, "Node");
            target.TakeDamage(damageToDeal);
            Destroy(gameObject);
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

    public void SetCounter(string c)
    {
        counter = c;
    }
    
    public void SetDamageAdditionPercent(float da)
    {
        damageAddtionPercent = da;
    }
}