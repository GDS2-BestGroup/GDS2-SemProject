using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    private Animator animator;
    private UnitBase unitStats;
    public bool trigger = false; //A bool to replace OnTriggerStay2D, it is not called every frame
    
    private Vector2 destination;
    private float walkSpeed;
    private float attackSpeed;
    private float attackInterval; //Timer for attack speed

    private List<UnitBase> targetList = new List<UnitBase>();
    // Start is called before the first frame update
    void Start()
    {
        unitStats = GetComponentInParent<UnitBase>();
        animator = GetComponentInParent<Animator>();
        walkSpeed = unitStats.walkSpeed;
        attackSpeed = unitStats.attackSpeed;
        attackInterval = attackSpeed;
        destination = unitStats.destination;
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger)
        {
            Attack();
        }

        if (!trigger)
        {
            Move();
        }

        if(unitStats.health <= 0)
        {
            trigger = false;
            animator.SetTrigger("Death");
            walkSpeed = 0;
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            targetList.Add(collision.gameObject.GetComponent<UnitBase>());
            trigger = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            targetList.Remove(collision.gameObject.GetComponent<UnitBase>());
            if (targetList.Count == 0)
            {
                trigger = false;
            }
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
            animator.SetTrigger("Attack");
            attackInterval = attackSpeed;
        }

    }

    /// <summary>
    /// Move the Unit to the Destination 
    /// </summary>
    public void Move()
    {
        float speed = walkSpeed * Time.deltaTime;
        gameObject.transform.parent.position = Vector2.MoveTowards(transform.position, destination, speed);
    }
}
