using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveProjectileMovement : MonoBehaviour
{
    [SerializeField] AnimationCurve xCurve, yCurve;
    private Rigidbody2D rb;
    private float timeElapsed = 0;
    private bool started = false;

    private Vector2 startPosition;
    private Vector2 endPosition;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!started)
        {
            started = true;
            timeElapsed = 0;
            startPosition = transform.position;
        }

        else
        {
            timeElapsed += Time.deltaTime;

            rb.MovePosition(endPosition);

                    //startPosition.x + xCurve.Evaluate(timeElapsed),
                    //startPosition.y + yCurve.Evaluate(timeElapsed))
        }
    }

    public void setTarget(Vector2 end)
    {
        endPosition = end;
    }
}
