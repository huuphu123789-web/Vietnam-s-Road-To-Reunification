using System.Collections;
using NUnit.Framework;

using UnityEngine;

public class EThrowG : MonoBehaviour
{
   [SerializeField] private Rigidbody2D rb;
   [SerializeField] private Animator animator;
   [SerializeField] private Transform player;
   [SerializeField] private GameObject boomPrefabs,deadPre,deadbyGPre;
   [SerializeField] private Transform throwPoint;
   [SerializeField] private Transform groundCheckTransform;
   [SerializeField] private LayerMask groundCheckLayerMask;
   public int throwForceX;
   public int throwForceY;

   public float moveSpeed;
   public float jumpForceX;
   public float jumpForceY;
   public float lineOfSite;
   public float throwRange;
   public float delayThrow;
   float distancefromPLayer;
   bool isThrowing = false;
   bool isGround = false;

   public Vector3 offset;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player= GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    
    void Update()
    {   
        isGround = Physics2D.OverlapCircle(groundCheckTransform.position,0.1f,groundCheckLayerMask);

        FindPlayer();
        FaceToPlayer();
        distancefromPLayer = Vector2.Distance(player.transform.position, transform.position);
        Action();
    }

    public void Action()
    {
        if(distancefromPLayer > lineOfSite)
        {
            transform.position = Vector2.MoveTowards(transform.position,player.position,moveSpeed * Time.deltaTime);
            if(transform.position.x > player.position.x)
            {
                transform.localScale = new Vector3 (1,1,1);
            }
            else
            {
                transform.localScale = new Vector3 (-1,1,1);
            }
            animator.SetBool("isRun",true);
        }
        else if (distancefromPLayer < lineOfSite && distancefromPLayer > throwRange)
        {   
            animator.SetBool("isRun",false);
            if(!isThrowing)
            {
                StartCoroutine(ThrowBoom());
            }
        }   
        else if (distancefromPLayer <throwRange)
        {
            float jumpDir = Mathf.Sign(player.position.x - transform.position.x);
            if(isGround)
            {
                rb.AddForce(new Vector2 (-jumpDir *jumpForceX,jumpForceY),ForceMode2D.Impulse);
            }
        }
    }

    IEnumerator ThrowBoom()
    {
        isThrowing = true;
        throwForceX = Random.Range(15,25);
        int count = 0;
        while(count <= 1 )
        { 
            animator.SetTrigger("isThrow");
            yield return new WaitForSeconds(0.8f);
            float dirX = Mathf.Sign(player.position.x - throwPoint.position.x);
        Vector2 throwForce = new Vector2 (dirX *throwForceX,throwForceY);
        GameObject boom =Instantiate(boomPrefabs,throwPoint.transform.position,Quaternion.identity);
       
        boom.GetComponent<Rigidbody2D>().AddForce (throwForce,ForceMode2D.Impulse);
        boom.transform.localScale = new Vector3 (boom.transform.position.x > 0 ? -1:1,1,1);
        count ++ ;
        yield return new WaitForSeconds(delayThrow);
        }
        isThrowing = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,lineOfSite);
        Gizmos.DrawWireSphere(transform.position,throwRange);
    }
   public void FindPlayer()
{
    if (!player)
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (!player) return;
    }
}

public void FaceToPlayer()
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
           
        }
    }
}

