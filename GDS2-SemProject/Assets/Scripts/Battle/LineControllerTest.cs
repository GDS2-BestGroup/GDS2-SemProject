using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineControllerTest : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private LineController line;
    // Start is called before the first frame update

    void Start()
    {
        if (points != null)
        {
            line.SetUpLine(points);
        }
    }

}
