using UnityEngine;

public class EnemyBullet : MonoBehaviour
{


    private Transform player;
    public float bulletSpeed = 5f;
    [SerializeField] Rigidbody2D bulletRb;
    void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        FindPlayer(); 
        if (player != null && !player.Equals(null)) 
        { 
            Vector2 moveDir = (player.position - transform.position).normalized * bulletSpeed; bulletRb.linearVelocity = new Vector2(moveDir.x, 0); 
        } 
        else
            { 
            
            }
        Destroy(gameObject, 2f);
    }
    void FindPlayer()
    {
        GameObject found = GameObject.FindGameObjectWithTag("Player");
        if (found != null) { player = found.transform; }
        else 
        {
             
        }
    }
}
