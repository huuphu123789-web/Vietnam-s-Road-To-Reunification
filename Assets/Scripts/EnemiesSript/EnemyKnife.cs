using System.Collections;
using UnityEngine;

public class EnemyKnife : MonoBehaviour
{
    [SerializeField] private float attackRange = 2;
    [SerializeField] private float attackZone ;
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    
    [SerializeField] private float lineofSite;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackDelay;
    [SerializeField] private float doDelay;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask players;
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private GameObject knifeDeathPrefab;
    [SerializeField] private GameObject knifeDeathByG;
    private float distanceformPlayer;
    bool isAttack = false;
    Vector3 offset=new Vector3(3, 0,0);
    void Start()
    {
         rb= GetComponent<Rigidbody2D>();
         animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceformPlayer= Vector2.Distance(transform.position,player.position);
        if (player == null || player.Equals(null))
        { FindPlayer(); return; }
        FaceToPlayer();
        EnemyRunToPlayer();
        
        if(!isAttack)
        {
            StartCoroutine(DoAttack());
        }
    }

IEnumerator EnemyAttack()
    {
        int swing = 0;
        while(swing < 1)
        {
            if (player == null || player.Equals(null))
            {
                yield break;
            }
        distanceformPlayer = Vector2.Distance(transform.position,player.position);

        if(distanceformPlayer <= attackRange )
        {
            animator.SetTrigger("isAttack");
            swing++;
            
        }
       
        yield return new WaitForSeconds(attackDelay);
        
        }
    }
IEnumerator DoAttack()
    {
        isAttack = true;
        yield return StartCoroutine(EnemyAttack());
        yield return new WaitForSeconds(doDelay);
        isAttack=false;
    }
    public void EnemyRunToPlayer()
    {
        FindPlayer();
        if(distanceformPlayer > attackRange && distanceformPlayer < lineofSite) 
        {
            animator.SetBool("isRun",true);
            transform.position=Vector2.MoveTowards(transform.position,player.position ,moveSpeed *Time.deltaTime);
        }
        else
        {
            animator.SetBool("isRun",false);
        }
    }
    void FindPlayer()
    {
        GameObject found = GameObject.FindGameObjectWithTag("Player");
        if (found != null)
        {
            player = found.transform;
        }
       
    }
    void FaceToPlayer()
    {
        if (player == null || player.Equals(null)) return;
        if (transform.position.x > player.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void HitPlayer()
    {
        Collider2D[] playerss = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, players);
       foreach (Collider2D player in playerss) 
        { 
            PlayController.instance.playerHp = PlayController.instance.playerHp - 2 ;
            AudioManager.instance.GettingHitByKnife();
        }
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lineofSite);
        Gizmos.DrawWireSphere(transform.position,attackRange);
        Gizmos.DrawWireSphere(transform.position,attackZone);
        Gizmos.DrawWireSphere(attackPoint.transform.position,radius);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Instantiate(knifeDeathPrefab,transform.position,Quaternion.identity);
            AudioManager.instance.KnifeDeath();
            Destroy(gameObject);
        }
        else if (other.CompareTag("Grenade"))
        {
            Instantiate(knifeDeathByG, transform.position, Quaternion.identity);
            AudioManager.instance.EnemyDeathByG();
            Destroy(gameObject);

        }
    }
}
