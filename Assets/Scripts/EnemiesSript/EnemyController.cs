using System.Collections;
using NUnit.Framework;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject enemyBulletPre;
    [SerializeField] private GameObject enemyDeathPre;

    [SerializeField] private GameObject enemyDeathByG;


    [SerializeField] private Transform firePoint;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float bulletDelay = 1f;
    [SerializeField] private float shootDelay = 3f;
    [SerializeField] private float lineofSite;
    [SerializeField] private float shootingRange;

    [SerializeField] private float attackRange;
    float distanceformPlayer;
    bool isActing = false;



    public static EnemyController instance;
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
       
        if (player == null || player.Equals(null))
        { FindPlayer(); return; }
        FaceToPlayer();

        if (!isActing)
        {
            StartCoroutine(DoAction());
        }
        EnemyRun();
       


    }


    IEnumerator DoAction()
    {
        isActing = true;

        StartCoroutine(EnemyShoot());

        yield return new WaitForSeconds(shootDelay);

        isActing = false;
    }

    IEnumerator EnemyShoot()
    {
        int ammo = 0;
        while (ammo < 1)
        {
            if (player == null || player.Equals(null))
            {

                yield break;
            }
            float distanceformPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceformPlayer <= shootingRange)
            {
                animator.SetTrigger("isShoot");
                Instantiate(enemyBulletPre, firePoint.position, Quaternion.identity);
                ammo++; AudioManager.instance.EnemyShoot();
                
            }


            yield return new WaitForSeconds(bulletDelay);
        }
    }

    
    void EnemyRun()
    {

        FindPlayer();
        distanceformPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceformPlayer < lineofSite && distanceformPlayer > shootingRange)
        {
            animator.SetBool("isRun", true);
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isRun", false);
        }
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineofSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
        
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
    void FindPlayer()
    {
        GameObject found = GameObject.FindGameObjectWithTag("Player");
        if (found != null)
        {
            player = found.transform;
        }
        else
        {

        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {

            Instantiate(enemyDeathPre, transform.position, Quaternion.identity);
            AudioManager.instance.EnemyDeath();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Grenade"))
        {
            Instantiate(enemyDeathByG, transform.position, Quaternion.identity);
            AudioManager.instance.EnemyDeathByG();
            Destroy(other.gameObject);
            Destroy(gameObject);

        }
    }



}
