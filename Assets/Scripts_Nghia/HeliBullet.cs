using UnityEngine;

public class HeliBullet : MonoBehaviour
{
    [Header("Move Before Chase")]
    public float moveSpeed = 5f;
    public float moveTime = 1.2f;

    [Header("Chase Settings")]
    public float chaseSpeed = 7f;
    public int damage = 1;

    [Header("Explosion")]
    public float explosionRadius = 1f;
    public GameObject explosionFX;
    public LayerMask damageLayer;
    public LayerMask hitLayer;

    private float timer;
    private bool isChasing = false;
    private Vector2 moveDirection;
    private Transform player;

    public void SetDirection(float direction)
    {
        moveDirection = new Vector2(direction, 0);
        RotateToDirection(moveDirection);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!isChasing)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            if (timer >= moveTime)
                isChasing = true;
        }
        else
        {
            if (player != null)
            {
                Vector2 dir = (player.position - transform.position).normalized;

                transform.Translate(dir * chaseSpeed * Time.deltaTime);

                RotateToDirection(dir);
            }
        }
    }

    void RotateToDirection(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 180f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //collision.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            Explode();
        }

        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Explode();
        }
    }

    void Explode()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        Animator anim = GetComponent<Animator>();
        if (anim != null)
            anim.enabled = false;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.enabled = false;

        if (explosionFX)
        {
            GameObject fx = Instantiate(explosionFX, transform.position, Quaternion.identity);
            Destroy(fx, 0.5f);
        }

        Destroy(gameObject);
    }
}