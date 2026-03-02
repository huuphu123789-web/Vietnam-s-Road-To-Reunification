
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class EnemyFlyAttack : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float forwardForce;
    [SerializeField] private Transform findPlayer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform groundCheckTransform,hitPoint;
    [SerializeField] private LayerMask groundCheckPLayerMask,player;
    [SerializeField] private GameObject deathPrefabs,deadbyGPre;
    [SerializeField] private int delayAction;
    public Vector3 offset;

    bool isGround = false;
    bool isJump= false;
    public float hitBox;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        findPlayer = GameObject.FindGameObjectWithTag("Player").transform;
       
    }

    // Update is called once per frame
    void Update()
    {   

        isGround = Physics2D.OverlapCircle(groundCheckTransform.position, 0.1f, groundCheckPLayerMask);
        if(isGround)
        {
            animator.SetBool("isRun",true);
        }
        else
        {
            animator.SetBool("isRun",false);
        }
        
        
        StartCoroutine(MoveToPlayer());
        FindPlayer();
       
        
        
        
    }
    
    public void FindPlayer()
    {
        if (findPlayer == null)
        {
            findPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    IEnumerator MoveToPlayer()
{
    
    
        rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
        transform.localScale = new Vector3(1,1,1);
        yield return new WaitForSeconds(1);
        if(isGround && !isJump)
            {
                rb.AddForce(new Vector2(forwardForce, jumpForce) , ForceMode2D.Impulse);
                animator.SetBool("isPrepare",true);
                yield return new WaitForSeconds(1);
                animator.SetBool("isPrepare",false);
                animator.SetBool("isAttack",true);
                yield return new WaitForSeconds(0.7f);
                animator.SetBool("isAttack",false);
            }
            isJump=true;
            yield return new WaitForSeconds(delayAction);
            rb.linearVelocity=new Vector2(-moveSpeed,rb.linearVelocity.y);
        
     
    Destroy(gameObject,2);
    
   
}
    public void HitPlayer()
    {
        Collider2D[] players = Physics2D.OverlapCircleAll(hitPoint.transform.position,hitBox,player);
        foreach(Collider2D playerss in players)
        {
            PlayController.instance.playerHp = PlayController.instance.playerHp - 2 ;
            AudioManager.instance.GettingHitByKnife();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bullet"))
        {
            Instantiate(deathPrefabs,transform.position,Quaternion.identity);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Grenade"))
        {
            Instantiate(deadbyGPre,transform.position + offset,Quaternion.identity);
            Destroy(gameObject);
            
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitPoint.position,hitBox);
    }


}
