using UnityEngine;

public class RocketAirStrike : MonoBehaviour
{
    [Header("Height")]
    public float riseHeight = 10f;
    public float riseSpeed = 8f;

    [Header("Fall")]
    public float fallSpeed = 20f;
    public float minDistance = 15f;
    public float maxDistance = 30f;

    [Header("Explosion")]
    public float explosionRadius = 2f;
    public GameObject explosionFX;
    public LayerMask damageLayer;
    public LayerMask hitLayer; // layer ground + player

    private Vector3 startPos;
    private Vector3 targetPos;

    private bool isRising = true;
    private bool isFalling = false;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        startPos = transform.position;
    }

    void Update()
    {
        if (isRising)
        {
            transform.position += Vector3.up * riseSpeed * Time.deltaTime;

            if (transform.position.y >= startPos.y + riseHeight)
            {
                PrepareFall();
            }
        }
        else if (isFalling)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                fallSpeed * Time.deltaTime
            );
        }
    }

    void PrepareFall()
    {
        isRising = false;

        float randomDistance = Random.Range(minDistance, maxDistance);
        int dir = Random.value > 0.5f ? 1 : -1;

        float targetX = startPos.x + randomDistance * dir;

        // rơi xuống thấp hơn để chắc chắn chạm ground
        targetPos = new Vector3(targetX, startPos.y - 50f, 0);

        anim.SetTrigger("Fall");

        Invoke(nameof(StartFalling), 0.1f);
    }

    void StartFalling()
    {
        isFalling = true;
    }

    // 💥 CHẠM LÀ NỔ
    void OnTriggerEnter2D(Collider2D other)
    {
        if ((hitLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            Explode();
        }
    }

    void Explode()
    {
        // Tắt collider để không kích hoạt nhiều lần
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        // Tắt animator
        Animator anim = GetComponent<Animator>();
        if (anim != null)
            anim.enabled = false;

        // Ẩn sprite ngay lập tức
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.enabled = false;

        // Spawn hiệu ứng nổ
        if (explosionFX)
        {
            GameObject fx = Instantiate(explosionFX, transform.position, Quaternion.identity);
            Destroy(fx, 0.5f); // xoá FX sau 2 giây
        }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}