using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    public float attackSpeed;
    public float health;
    public float damage;
    public float armor;
    public string unitName;

    public float attackTimeBetween;

    public void Start()
    {
        attackTimeBetween = attackSpeed;
    }

    public void Update()
    {
        
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            attackTimeBetween -= Time.deltaTime;
            if (attackTimeBetween <= 0)
            {
                Debug.Log("Attack");
                attackTimeBetween = attackSpeed;
                //Player Attack Animation
            }
        }
    }
}

