using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float velocity;
    private float lifeTime;
    [SerializeField] private Transform destination;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lifeTime = 30;
    }

    // Update is called once per frame
    void Update()
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
        if (destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination.position, speed);
        }
    }

    public void SetDestination(Transform d)
    {
        destination = d;
        Debug.Log("destination set");
    }

    public void SetVelocity(float v)
    {
        velocity = v;
    }
}
