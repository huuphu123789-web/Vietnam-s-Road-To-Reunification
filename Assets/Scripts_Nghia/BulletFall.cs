using UnityEngine;

public class BulletParabola : MonoBehaviour
{
    public float launchForce = 8f;   // lực bắn tổng
    public float angle = 45f;        // góc bắn (độ)

    private Rigidbody2D rb;

    public void Shoot(float direction)
    {
        rb = GetComponent<Rigidbody2D>();

        float rad = angle * Mathf.Deg2Rad;

        float vx = Mathf.Cos(rad) * launchForce * direction;
        float vy = Mathf.Sin(rad) * launchForce;

        rb.linearVelocity = new Vector2(vx, vy);
    }

    void Update()
    {
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            float rot = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot);
        }
    }
}