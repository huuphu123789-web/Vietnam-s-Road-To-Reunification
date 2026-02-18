using System.Collections;
using UnityEngine;

public class EnemyOutCombatController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float runSpeed = 7;
    [SerializeField] private float alarmRange;
    float distanceToPlayer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        StartCoroutine(CheckPlayerInRange());
    }

    IEnumerator CheckPlayerInRange()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < alarmRange)
        {
            animator.SetBool("isScare", true);
            yield return new WaitForSeconds(0.4f);
            RunAway();

        }


    }

    public void RunAway()
    {

        rb.linearVelocity = Vector2.right * runSpeed;
        transform.localScale = new Vector3(-1, 1, 1);
        animator.SetBool("isScare", false);
        animator.SetBool("isRunAway", true);


    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, alarmRange);
    }
}
