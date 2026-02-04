using UnityEngine;

public class BoomSript : MonoBehaviour
{
    public float throwForceX = 9f;
    public float throwForceY = 3f; 
    public float spinForce = 200f;  
    
    public GameObject explosionPrefab;   
    [SerializeField] private Rigidbody2D rb;
    private Vector3 offset ;
    
         
    Vector2 direction;
    public static BoomSript instance;

    void Awake()
    {
        offset = new  Vector3(0,4,0);
        rb.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    }

   void OnTriggerEnter2D(Collider2D other)
    {
   if (other.CompareTag("Boss"))
    {
            Explode();

    } 
   else if(other.CompareTag("Ground"))
        {
            Explode();
        }
  else if (other.CompareTag("Enemy"))
        {
            Explode();
        }
        else if (other.CompareTag("EnemyKnife"))
        {
            Explode();
        }
        else if(other.CompareTag("Toilet"))
        {
            Explode();
        }
    }
    void Explode()
    {
       
        Instantiate(explosionPrefab, transform.position + offset, Quaternion.identity);
        Destroy(gameObject);
        AudioManager.instance.BoomExplosionClip();
    }
    }



