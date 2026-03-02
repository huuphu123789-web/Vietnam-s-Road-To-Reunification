using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bombPrefab;
    [SerializeField] GameObject bossDeathPre;
    [SerializeField] Transform firePoint;
    [SerializeField] Transform throwBoom;
    [SerializeField] Transform player;
    [SerializeField] Transform groundcheckTransform;
    [SerializeField] public LayerMask groundcheckLayer;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float throwForceX;
    [SerializeField] float throwForceY;
    Vector2 direction;
    [SerializeField] Animator animator;
    [SerializeField] float moveSpeed = 6;
    [SerializeField] float burstDelay = 0.5f;
    [SerializeField] float actionDelay = 2f;
    public Transform pointA;
    public Transform pointB;

    [SerializeField] public float jumpForce = 5f;
    [SerializeField] public float horizontalForce = 6f;

    [SerializeField] int bossHp = 10;
    [SerializeField] float timetoAction = 0;

    PlayController playController;

    private bool isActing = false;
    private bool isGrounded;
    private bool isAttack;
    public static BossController instance;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        playController = FindAnyObjectByType<PlayController>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rb.gravityScale = 0.5f;
        StartCoroutine(WaitForBossLaugh());
    }

    void Update()
    {
        WaitForAttack();
        FindPLayer();
        BossGroundCheck();
        FaceToPlayer();

        if (!isActing && isGrounded && isAttack == true)
        {
            StartCoroutine(DoRandomAction());
        }

        CheckBossDeath();
    }
    IEnumerator WaitForBossLaugh()
    {
        yield return new WaitForSeconds(2);
        AudioManager.instance.BossLaugh();
    }
    void FaceToPlayer()
    {
        if (player == null)
        {
            PlayController found = FindFirstObjectByType<PlayController>();
            if (found != null)
            {
                player = found.transform;
            }
            else
            {
                Debug.LogWarning("Boss không tìm thấy Player."); return;
            }
        }
        if (rb.position.x > player.position.x)
        { transform.localScale = new Vector3(1, 1, 1); }
        else if (rb.position.x < player.position.x)
        { transform.localScale = new Vector3(-1, 1, 1); }
    }

    bool BossGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundcheckTransform.position, 0.5f, groundcheckLayer);
        return isGrounded;
    }
    public void ShootBurst()
    {
        StartCoroutine(BurstFire());
    }


    public void WaitForAttack()
    {

        isAttack = false;
        timetoAction += Time.deltaTime;

        if (timetoAction < 7f)
        {
            if (animator != null && !animator.Equals(null))
                animator.SetTrigger("isLanding");
        }
        else if (timetoAction > 10f)
        {
            if (rb != null && !rb.Equals(null)) rb.gravityScale = 1; isAttack = true;
            if (playController != null && !playController.Equals(null))
                playController.SetTrue();
        }

    }
    IEnumerator DoRandomAction()
    {

        isActing = true;

        int action = Random.Range(0, 4);

        switch (action)
        {
            case 0:
                yield return MoveBetweenPoints();
                break;
            case 1:
                ShootBurst();
                break;
            case 2:
                ThrowBomb();
                break;
            case 3:
                yield return JumpBetweenPoints();
                break;
        }

        yield return new WaitForSeconds(actionDelay);
        isActing = false;
    }
    IEnumerator BurstFire()
    {
        int bulletsShot = 0;
        while (bulletsShot < 3)
        {
            animator?.SetTrigger("isBossFireNgang");
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            AudioManager.instance?.BossShootClip();
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (player != null && !player.Equals(null))
            {
                if (player.position.x < transform.position.x)
                    bullet.transform.localScale = new Vector3(-1, 1, 1);
                else
                    bullet.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                Debug.LogWarning("Không có Player để xác định hướng đạn.");
            }
            bulletsShot++; yield return new WaitForSeconds(burstDelay);
        }

    }
    IEnumerator MoveBetweenPoints()
    {

        if (isGrounded)
        {
            Transform target = (Vector2.Distance(transform.position, pointA.position) <
                                Vector2.Distance(transform.position, pointB.position)) ? pointB : pointA;

            while (Vector2.Distance(transform.position, target.position) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                animator.SetBool("isBossRun", true);
                yield return null;

            }
            transform.position = target.position;
            animator.SetBool("isBossRun", false);
        }
    }
    void ThrowBomb()
    {

        GameObject bomb = Instantiate(bombPrefab, throwBoom.position, Quaternion.identity); 
        animator?.SetTrigger("isBossThrow"); 
        GameObject found = GameObject.FindGameObjectWithTag("Player"); 
        if (found != null) { player = found.transform; 
        Vector2 direction = (player.position - firePoint.position).normalized; 
        Vector2 throwForceXY = new Vector2(throwForceX, throwForceY); 
        bomb.GetComponent<Rigidbody2D>().AddForce(direction * throwForceXY, ForceMode2D.Impulse); 
        if (player.position.x < transform.position.x) bomb.transform.localScale = new Vector3(-1, 1, 1); 
        else bomb.transform.localScale = new Vector3(1, 1, 1); } 
        else { Debug.LogWarning("Không tìm thấy Player. Đạn sẽ không định hướng."); }
    }

    IEnumerator JumpBetweenPoints()
    {

        if (isGrounded)
        {
            Transform target = (Vector2.Distance(transform.position, pointA.position) <
                            Vector2.Distance(transform.position, pointB.position)) ? pointB : pointA;

            Vector2 direction = (target.position - transform.position).normalized;
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(new Vector2(direction.x * 5f, 10f), ForceMode2D.Impulse);
            animator.SetTrigger("isBossJump");


        }
        while (!isGrounded)
        {
            yield return null;
        }


    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Bullet"))
        {
            bossHp = bossHp - 1;

        }
        else if (other.CompareTag("Grenade"))
        {
            bossHp = bossHp - 5;

        }
    }

    public void CheckBossDeath()
    {
        if (bossHp <= 0)
        {
            Instantiate(bossDeathPre, transform.position, Quaternion.identity);
            AudioManager.instance.BossDeath();
            SpwaningCritpt.instance.Victory();
            Destroy(gameObject);
        }
    }

    public void FindPLayer()
    {
        if (player == null)
        {
            PlayController found = GameObject.FindAnyObjectByType<PlayController>();
            if (found != null)
            {
                player = found.transform; Debug.Log("Enemy đã tìm thấy Player mới.");
            }
            else
            {
                Debug.LogWarning("Không tìm thấy Player.");
            }
        }
    }

    public void OnDestroy()
    {
        if (PlayController.instance != null)
        {
            PlayController.instance.animator.SetBool("isVictory", true);
            PlayController.instance.isMove = false;
            PlayController.instance.isShooting = false;
            if (SpwaningCritpt.instance.victoryPanel != null)
            {
                SpwaningCritpt.instance.victoryPanel.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Victory Panel chưa được gán trong PlayController");
            }
        }
        else { Debug.LogWarning("PlayController.instance bị null trong OnDestroy"); }
        if (SpwaningCritpt.instance != null)
        {
            SpwaningCritpt.instance.Victory();
        }
        else
        {
            Debug.LogWarning("SpwaningCritpt.instance bị null");
        }
        SpwaningCritpt.instance.DestroyAllEnemies();
        SpwaningCritpt.instance.DestroyAllEnemyBullet();
        SpwaningCritpt.instance.DestroyAllEnemyKnife();

    }
}



