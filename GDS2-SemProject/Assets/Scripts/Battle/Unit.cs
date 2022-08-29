using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private float uSpawnSpeed = 2;
    float movespeed = 3;
    public Transform destination;
    public bool summoned = false;
    GameObject parnet;
    float life = 4;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (summoned)
        {
            var step = movespeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destination.position, step);
        }

        life -= Time.deltaTime;
        if(life <= 0)
        {   
            Destroy(gameObject);
        }
    }
    
    public float GetSpawnSpeed()
    {
        return uSpawnSpeed;
    }

    public void SpawnUnit(Transform start, Transform end)
    {
        Unit l = Instantiate(this, start.position, Quaternion.identity);
        l.destination = end;
        l.summoned = true;
    }

    private void MoveUnit()
    {

    }
}
