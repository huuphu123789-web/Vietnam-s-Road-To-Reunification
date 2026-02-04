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
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.position);
        StartCoroutine(CheckPlayerInRange());
    }

    IEnumerator CheckPlayerInRange()
    {
        if (distanceToPlayer < alarmRange)
        {
            animator.SetBool("isScare", true);
            yield return new WaitForSeconds(1);
        }
        RunAway();

        animator.SetBool("isRunAway", true);
        transform.localScale = new Vector3(1, 0, 0);
    }

    public void RunAway()
    {
        rb.linearVelocity = new Vector2(runSpeed * Time.deltaTime, rb.linearVelocity.y);

    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, alarmRange);
    }
}
