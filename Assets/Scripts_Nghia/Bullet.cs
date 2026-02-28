using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public float lifeTime = 2f;

    [Header("Spawn Spread")]
    public float spawnSpread = 0.1f; // độ lệch +- tại điểm bắn

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 direction)
    {
        direction = direction.normalized;

        // 🔹 lệch vị trí spawn (vuông góc hướng bắn)
        Vector2 perpendicular = new Vector2(-direction.y, direction.x);
        float offset = Random.Range(-spawnSpread, spawnSpread);
        transform.position += (Vector3)(perpendicular * offset);

        // 🔹 bay thẳng
        rb.linearVelocity = direction * speed;

        // 🔹 xoay viên đạn theo hướng bay
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
