using System.Collections;
using UnityEngine;

public class _6NongScript : MonoBehaviour
{
   [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    [SerializeField] private Transform player,groundCheckTransform,firePoint;
    [SerializeField] private LayerMask groundCheckLayerMask;

    [SerializeField] private GameObject _6NongBulletPre,deadPre,deadbyGPre,gunSmoke;
    public Vector3 offset,offsetG;
    public float lineOfSite,shootingRange;
    public float bulletForce,delayShooting,moveSpeed,reloadtime;

    float distanceFromPlayer;
    bool isGround=false;
    bool isAttack=false;
    bool isReload=false;
    int bulletCount=0;
    
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
            animator.SetBool("isRun",true);
        }
        else if(distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange)
        {

            if(!isAttack)
            {
                StartCoroutine(Shoot());
            }
            
        }

    }
    IEnumerator Shoot()
    {
        if (isReload) yield break;
        isAttack = true;
        
        int bullet = 0;
        bulletCount = 0;
        while(bullet < 6)
        {
            float shootdir = Mathf.Sign(player.position.x - transform.position.x);
            Vector2 bulletdir = new Vector2 (shootdir * bulletForce,0);
            animator.SetBool("isRun",false);
            animator.SetTrigger("isShoot");
            GameObject _6bullet =  Instantiate(_6NongBulletPre,firePoint.position,Quaternion.identity);
            GameObject _smoke = Instantiate(gunSmoke,firePoint.position,Quaternion.identity);
            _6bullet.GetComponent<Rigidbody2D>().AddForce(bulletdir,ForceMode2D.Impulse);
            bullet++;
            bulletCount++;
            if(bulletCount == 5)
            {
                StartCoroutine(Reload());
            }
            yield return new WaitForSeconds(delayShooting);
        }
        
       isAttack=false;
        
    }

    IEnumerator Reload()
    {
        isReload=true;
        isAttack=false;
        animator.SetBool("isReload",true);
        yield return new WaitForSeconds(reloadtime);
        animator.SetBool("isReload",false);
        bulletCount=0;
        isReload=false;
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
            Instantiate(deadbyGPre,transform.position,Quaternion.identity);
            Destroy(gameObject);
        
        }
    }




}
