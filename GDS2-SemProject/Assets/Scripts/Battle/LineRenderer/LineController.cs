using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lr;
    private Transform[] points;

    // Start is called before the first frame update
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame

    public void SetUpLine(Transform[] p)
    {
        if (p != null && p.Length >= 2)
        {
            lr.positionCount = p.Length;
            points = p;

            for (int i = 0; i < points.Length; i++)
            {
                lr.SetPosition(i, points[i].position);
            }
        }
    }
}
