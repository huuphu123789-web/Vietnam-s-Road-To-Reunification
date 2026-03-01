using UnityEngine;

public class Boom : MonoBehaviour
{
    [Header("Bounce")]
    public float bounceForce = 4f;
    public float bounceDecay = 0.7f;

    [Header("Explode Rule")]
    public int maxGroundHit = 2;
    public float explodeAfterSeconds = 2f;

    [Header("Explosion")]
    public GameObject explosionFX;
    public float damageRadius = 1.5f;
    public LayerMask groundLayer;

    Rigidbody2D rb;
    int groundHitCount = 0;
    bool exploded = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        Invoke(nameof(Explode), explodeAfterSeconds);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (exploded) return;

        if (col.collider.CompareTag("Enemy"))
        {
            Explode();
            return;
        }

        if (((1 << col.gameObject.layer) & groundLayer) != 0)
        {
            groundHitCount++;

            bounceForce *= bounceDecay;
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                bounceForce
            );

            if (groundHitCount >= maxGroundHit)
            {
                Explode();
            }
        }
    }

    void Explode()
    {
        if (exploded) return;
        exploded = true;

        CancelInvoke();

        if (explosionFX)
        {
            GameObject fx = Instantiate(
                explosionFX,
                transform.position,
                Quaternion.identity
            );

            Animator anim = fx.GetComponent<Animator>();
            if (anim)
            {
                float t = anim.GetCurrentAnimatorStateInfo(0).length;
                Destroy(fx, t);
            }
            else
            {
                Destroy(fx, 0.5f);
            }
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            damageRadius
        );

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                // hit.GetComponent<Enemy>()?.TakeDamage(50);
            }
        }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
