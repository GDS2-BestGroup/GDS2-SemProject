using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private Vector3 rot;
    // Start is called before the first frame update
    void Start()
    {
        rot = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        rot.z-= 5;
        transform.eulerAngles = rot;
    }
}
