using UnityEngine;

public class BossBoom : MonoBehaviour
{
    public float throwForceX = 9f;
    public float throwForceY = 3f;
    public float spinForce = 200f;
    public GameObject explosionPrefab;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] Transform player;
    private Vector3 offsset;

    Vector2 direction;


    void Awake()
    {
        offsset = new Vector3 (0,4,0);
        rb.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GameObject found = GameObject.FindGameObjectWithTag("Player");
        if (found != null)
        {
            player = found.transform;
            if (transform.position.x > player.position.x)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            Debug.LogWarning("BossBoom không tìm thấy Player.");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Explode();
        }
        else if (other.CompareTag("Ground"))
        {
            Explode();
        }

    }
    void Explode()
    {

        Instantiate(explosionPrefab, transform.position + offsset, Quaternion.identity);
        Destroy(gameObject); 
        AudioManager.instance.BoomExplosionClip();
    }
}
