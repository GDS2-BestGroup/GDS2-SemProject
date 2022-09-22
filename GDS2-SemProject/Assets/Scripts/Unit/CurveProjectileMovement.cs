using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveProjectileMovement : MonoBehaviour
{
    [SerializeField] float velocity;
    private float lifeTime;
    private Transform destination;
    // Start is called before the first frame update
    void Start()
    {
        lifeTime = 30;
    }

    private void FixedUpdate()
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
        Vector3 relativePos = destination.position - transform.position;

        if (destination)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination.position + offSet, speed);
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector2.up);
            transform.rotation = rotation;
        }

        else
        {
            Debug.Log("No Destination");
            Destroy(gameObject);
        }

    }
    public void setTarget(Transform end)
    {
        destination = end;
    }
}
