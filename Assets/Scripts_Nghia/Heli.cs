using UnityEngine;

public class Heli : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 3f;

    private bool goingToB = true;

    void Update()
    {
        Vector2 target = goingToB ? pointB.position : pointA.position;

        transform.position = Vector2.MoveTowards(
            transform.position,
            target,
            moveSpeed * Time.deltaTime
        );

        float direction = target.x - transform.position.x;
        Flip(direction);

        if (Vector2.Distance(transform.position, target) < 0.05f)
        {
            goingToB = !goingToB;
        }
    }

    void Flip(float direction)
    {
        if (direction > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);   
        }
        else if (direction < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);  
        }
    }
}