using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] float velocity;
    private float lifeTime;
    [SerializeField] private Transform destination;
    // Start is called before the first frame update
    void Start()
    {
        lifeTime = 3;
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
        Vector3 offSet = new Vector3(0, 0.17f, 0);
        if (destination)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination.position + offSet, speed);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, Vector2.right, speed);
        }
    }

    public void setTarget(Transform end)
    {
        destination = end;
    }

    public void SetVelocity(float v)
    {
        velocity = v;
    }

}
