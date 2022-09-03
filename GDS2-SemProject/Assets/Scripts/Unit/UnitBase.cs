using System.Collections;
using System.Collections.Generic;
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
    private float attackInterval; //Timer for attack speed
    public bool isPlayer; //Is player unit or not

    private Animator animator;
    private bool trigger = false; //A bool to replace OnTriggerStay2D, it is not called every frame
    public Vector2 destination;
    public void Start()
    {
        attackInterval = attackSpeed;
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (health <= 0)
        {
            animator.SetTrigger("Death");
        }

        if (trigger)
        {
            Attack();
        }

        if (!trigger)
        {
            Move();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch(isPlayer)
        {
            case true:
                if (collision.gameObject.tag == "Enemy")
                {
                    trigger = true;
                }
                break;

            case false:
                if (collision.gameObject.tag == "Player")
                {
                    trigger = true;
                }
                break;
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        switch (isPlayer)
        {
            case true:
                if (collision.gameObject.tag == "Enemy")
                {
                    trigger = false;
                }
                break;

            case false:
                if (collision.gameObject.tag == "Player")
                {
                    trigger = false;
                }
                break;
        }

    }
    /// <summary>
    /// Play the attack animation
    /// </summary>
    public void Attack()
    {
        attackInterval -= Time.deltaTime;
        if (attackInterval <= 0)
        {
            Debug.Log("Attack");
            animator.SetTrigger("Attack");
            attackInterval = attackSpeed;
        }

    }

    public void Move()
    {
        float speed = walkSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, destination, speed);
    }
}

