using System.Collections;
using UnityEngine;

public class EBazooKaScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    [SerializeField] private Transform player,groundCheckTransform,firePoint;
    [SerializeField] private LayerMask groundCheckLayerMask;

    [SerializeField] private GameObject bazookaBulletPre,deadPre,deadbyGPre;
    public Vector3 offset;
    public float lineOfSite,shootingRange;
    public float jumpForceX,jumpForceY,bulletForce,delayShooting,moveSpeed;

    float distanceFromPlayer;
    bool isGround=false;
    bool isAttack=false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player= GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayer();
        FaceToPLayer();
        
        distanceFromPlayer =Vector2.Distance(player.transform.position,transform.position);
        isGround = Physics2D.OverlapCircle(groundCheckTransform.position,0.1f,groundCheckLayerMask);
        MoveToPlayer();
    }

    public void FindPlayer()
{
    if (!player)
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (!player) return;
    }
}

    public void FaceToPLayer()

    {
        if(transform.position.x > player.position.x)
        {
            transform.localScale = new Vector3 (1,1,1);
        }
        else
        {
            transform.localScale = new Vector3(-1,1,1);
        }
    }


    public void MoveToPlayer()
    {
        if(distanceFromPlayer > lineOfSite)
        {
            transform.position = Vector2.MoveTowards(transform.position,player.position,moveSpeed * Time.deltaTime);
            animator.SetBool("isJump",false);
            animator.SetBool("isRun",true);
            
        }
        else if(distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange)
        {

            if(!isAttack)
            {
                StartCoroutine(Shoot());
            }
            
        }
        else
        {
            if(isGround)
            {
                float jumpdir = Mathf.Sign(player.position.x - transform.position.x);
                rb.AddForce(new Vector2(-jumpdir * jumpForceX,jumpForceY),ForceMode2D.Impulse);
                animator.SetBool("isJump",true);
            }
        }

    }
    IEnumerator Shoot()
    {
        isAttack = true;
        
        int count = 0;
        while(count <= 1)
        {
            float shootdir = Mathf.Sign(player.position.x - transform.position.x);
            Vector2 bulletdir = new Vector2 (shootdir * bulletForce,0);
            animator.SetTrigger("isShooting");
            
            animator.SetBool("isRun",false);
            yield return new WaitForSeconds(0.5f);
            GameObject bazookabullet =  Instantiate(bazookaBulletPre,firePoint.position,Quaternion.identity);
            bazookabullet.GetComponent<Rigidbody2D>().AddForce(bulletdir,ForceMode2D.Impulse);
            count++;
            yield return new WaitForSeconds(delayShooting);
        }

        isAttack= false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,lineOfSite);
        Gizmos.DrawWireSphere(transform.position,shootingRange);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bullet"))
        {
            Instantiate(deadPre,transform.position + offset,Quaternion.identity);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Grenade"))
        {
            Instantiate(deadbyGPre,transform.position + offset,Quaternion.identity);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }




}
