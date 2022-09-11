using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttackDealer : MonoBehaviour
{
    private List<UnitBase> targetList = new List<UnitBase>();
    private float damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            targetList.Add(collision.gameObject.GetComponent<UnitBase>());
            UnitBase target = targetList[0];
            float damageToDeal = DealDamage(damage, target.GetDefense());
            target.TakeDamage(damageToDeal);
        }
        Destroy(gameObject);
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