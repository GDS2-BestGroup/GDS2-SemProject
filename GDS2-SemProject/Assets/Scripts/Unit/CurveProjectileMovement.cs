using UnityEngine;

public class CurveProjectileMovement : MonoBehaviour
{
    private Vector3 startPos;
    [SerializeField] private Transform dest;
    private float tick = 0;
    [SerializeField] private float timeToDest = 3;
    public float Percent => tick / timeToDest;

    [SerializeField] private Transform child;
    [SerializeField] private float height = 2;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        transform.right = dest.position - startPos;

    }

    private void Update()
    {
        float add = tick + Time.deltaTime;
        tick += add > timeToDest ? 0 : Time.deltaTime;
        Move();

    }
    public void Move()
    {
        if (dest)
        {
            Vector2 newPos = Vector2.Lerp(startPos, dest.position, Percent);
            transform.position = newPos;

            Vector2 localPos = new()
            {
                y = height * Mathf.Sin(Mathf.Deg2Rad * Percent * 180)
            };
            child.localPosition = localPos;

        }

        else
        {
            Debug.Log("No Destination");
            Destroy(gameObject);
        }

    }

    public void setTarget(Transform end)
    {
        dest = end;
    }
}
