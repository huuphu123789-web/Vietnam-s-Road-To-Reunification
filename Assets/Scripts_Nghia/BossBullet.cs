using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 8f;
    public Vector2 direction = Vector2.right;

    [Header("Life Time")]
    public float lifeTime = 5f;

    [Header("Damage")]
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}