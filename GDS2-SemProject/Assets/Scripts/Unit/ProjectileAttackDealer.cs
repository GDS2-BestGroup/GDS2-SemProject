using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttackDealer : MonoBehaviour
{
    private List<UnitBase> targetList = new List<UnitBase>();
    public float damage { get; set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //RayCast, no collider, cast a raycast, if hit move to the target.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 10f);

        if (hit)
        {
            if (hit.transform.tag == "Enemy" || hit.transform.tag == "Player")
            {
                Debug.Log("HIT");
            }
        }    
    }

    /*
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            targetList.Add(collision.gameObject.GetComponent<UnitBase>());
            UnitBase target = targetList[0];
            float damageToDeal = DealDamage(unitStats.damage, target.defense);
            target.health -= damageToDeal;
        }
        Destroy(gameObject);
    }

    */
    public float DealDamage(float damage, float defense)
    {
        float damageToDeal = (damage - defense);
        if (damageToDeal <= 0)
        {
            damageToDeal = 1;
        }

        return damageToDeal;
    }
}