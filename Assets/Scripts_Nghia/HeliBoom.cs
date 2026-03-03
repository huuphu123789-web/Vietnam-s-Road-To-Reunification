using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask groundLayer;
    public GameObject explosionFX;

    private float direction = 1f;

    void Start()
    {
        direction = Mathf.Sign(transform.localScale.x);
    }

    void Update()
    {
        transform.Translate(Vector2.down * direction * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
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