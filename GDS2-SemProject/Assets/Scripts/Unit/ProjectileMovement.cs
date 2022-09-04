using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float velocity;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = gameObject.transform.parent.tag;
        gameObject.layer = (gameObject.transform.parent.tag == "Player") ? LayerMask.NameToLayer("Player") : LayerMask.NameToLayer("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.forward, velocity * Time.deltaTime);

    }
}
