using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 3f;

    private bool goingToB = true;

    void Update()
    {
        if (goingToB)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                pointB.position,
                moveSpeed * Time.deltaTime
            );

            if (Vector2.Distance(transform.position, pointB.position) < 0.05f)
            {
                goingToB = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                pointA.position,
                moveSpeed * Time.deltaTime
            );

            if (Vector2.Distance(transform.position, pointA.position) < 0.05f)
            {
                goingToB = true;
            }
        }
    }
}