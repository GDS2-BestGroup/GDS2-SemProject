using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float velocity;
    private float lifeTime;
    private Transform destination;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lifeTime = 30;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Move()
    {
        float speed = velocity * Time.deltaTime;
        gameObject.transform.parent.position = Vector2.MoveTowards(transform.position, destination.position, speed);
    }
}
