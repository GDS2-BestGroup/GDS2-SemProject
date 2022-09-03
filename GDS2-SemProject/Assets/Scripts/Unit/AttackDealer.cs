using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDealer : MonoBehaviour
{
    private List<UnitBase> targetList = new List<UnitBase>();
    private UnitBase unitStats;

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
        switch (unitStats.isPlayer)
        {
            case true:
                if (collision.gameObject.tag == "Enemy")
                {
                    UnitBase target = collision.gameObject.GetComponent<UnitBase>();
                    float damageToDeal = DealDamage(unitStats.damage, target.defense);
                    target.health -= damageToDeal;
                }
                break;

            case false:
                if (collision.gameObject.tag == "Player")
                {
                    UnitBase target = collision.gameObject.GetComponent<UnitBase>();
                    float damageToDeal = DealDamage(unitStats.damage, target.defense);
                    target.health -= damageToDeal;
                }
                break;
        }
    }

    public float DealDamage(float damage, float defense)
    {
        Debug.Log("Deal Dmg");
        float damageToDeal = (damage - defense);
        if (damageToDeal <= 0)
        {
            damageToDeal = 1;
        }

        return damageToDeal;
    }
}
