
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
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask groundCheckPLayerMask;
    [SerializeField] private GameObject deathPrefabs;
    [SerializeField] private int delayAction;
    

    bool isGround = false;
    bool isJump= false;
    
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
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bullet"))
        {
            Instantiate(deathPrefabs,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }

    
}
