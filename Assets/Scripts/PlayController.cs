using System.Collections;
using NUnit.Framework;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] PlayController playController;
    [SerializeField] public Animator animator;
    [SerializeField] private LayerMask groundCheckLayerMask;
    [SerializeField] private Transform groundCheckTransForm;
    [SerializeField] private GameObject bulletPrefabs;

    [SerializeField] private GameObject boomPrefabs;
    [SerializeField] private GameObject deathPrefabs;
    [SerializeField] GameObject shield;



    [SerializeField] private Transform leftrightPoint;
    [SerializeField] private Transform upPoint;
    [SerializeField] private Transform downPoint;
    [SerializeField] private Transform throwPoint;
    Vector2 shootDirection;
    Vector2 throwDirection;
    Vector2 currentPosition;

    [SerializeField] float moveSpeed = 5;
    [SerializeField] float jumpForce = 10;
    [SerializeField] float bulletSpeed = 5;
    [SerializeField] public int playerHp = 100;
    [SerializeField] public int ammo = 10;

    [SerializeField] public int grenadeCount = 10;


    public float throwForceX = 5f;
    public float throwForceY = 3f;
    public float spinForce = 500f;

    public bool isGround;
    public bool isShooting = true;

    public bool isMove = true;
    public bool imotal = false;


    public static PlayController instance;

    SpwaningCritpt spwaningCritpt;


    void Awake()
    {

        instance = this;
        spwaningCritpt = FindFirstObjectByType<SpwaningCritpt>();
        playController = GetComponent<PlayController>();
        rb = GetComponent<Rigidbody2D>();
        currentPosition = Vector2.right;

        animator = GetComponent<Animator>();

        StartCoroutine(DisableShield());
    }


    void Update()
    {
        
        shootDirection = currentPosition;
        throwDirection = currentPosition;
        PlayerMove();
        PlayerJump();
        PlayerCrouch();
        PlayerAttack();
        PlayerThrowBoom();

        // UseKnife();




    }
    IEnumerator DisableShield()
    {
        yield return new WaitForSeconds(3);
        shield.SetActive(false);
    }



    public void PlayerMove()
    {
        if (isMove == true)
        {
            float x = Input.GetAxis("Horizontal");

            rb.linearVelocity = new Vector2(x * moveSpeed, rb.linearVelocity.y);
            if (x > 0)
            {
                currentPosition = Vector2.right;
                throwDirection = Vector2.right;
                transform.localScale = new Vector3(1, 1, 1);
                animator.SetBool("isWalk", true);
            }
            else if (x < 0)
            {
                currentPosition = Vector2.left;
                throwDirection = Vector2.left;
                transform.localScale = new Vector3(-1, 1, 1);
                animator.SetBool("isWalk", true);
            }
            else
            {
                animator.SetBool("isWalk", false);

            }
        }


    }

    public void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGround)
        {
            animator.SetBool("isJump", true);
            rb.AddForce(new Vector2(rb.linearVelocity.x, jumpForce));
        }
        else
        {
            animator.SetBool("isJump", !isGround);
        }
        isGround = Physics2D.OverlapCircle(groundCheckTransForm.position, 0.5f, groundCheckLayerMask);
    }

    public void PlayerCrouch()
    {

        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("isCrouch", true);
        }
        else
        {
            animator.SetBool("isCrouch", false);
        }
    }

    public void PlayerAttack()
    {


        if (Input.GetKey(KeyCode.W))
        {

            animator.SetBool("isLookUp", true);
            if (Input.GetMouseButtonDown(0) && isShooting)
            {

                shootDirection = Vector2.up;
                animator.SetTrigger("isShootUp");
                Shoot(shootDirection);


            }

        }
        else if (Input.GetKey(KeyCode.S) && isGround == false)
        {
            shootDirection = Vector2.down;
            if (Input.GetMouseButtonDown(0) && isShooting)
            {

                animator.SetTrigger("isLandAttack");
                Shoot(shootDirection);
            }



        }

        else if (Input.GetKey(KeyCode.S) && isGround == true)
        {
            shootDirection = currentPosition;
            if (Input.GetMouseButtonDown(0) && isShooting)
            {

                animator.SetTrigger("isCrouchShoot");
                Shoot(shootDirection);
            }


        }

        else if (Input.GetKey(KeyCode.A))
        {
            shootDirection = Vector2.left;
            if (Input.GetMouseButtonDown(0) && isShooting)
            {

                animator.SetTrigger("isShoot");
                Shoot(shootDirection);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            shootDirection = Vector2.right;

            if (Input.GetMouseButtonDown(0) && isShooting)
            {

                animator.SetTrigger("isShoot");
                Shoot(shootDirection);
            }
        }
        else if (Input.GetMouseButtonDown(0) && isShooting)
        {

            animator.SetTrigger("isShoot");
            Shoot(currentPosition);

        }
        else
        {
            animator.SetBool("isLookUp", false);
        }




    }

    void Shoot(Vector2 direction)
    {
        if (shootDirection == currentPosition)
        {
            GameObject bullet = Instantiate(bulletPrefabs, leftrightPoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = direction * bulletSpeed;
            AudioManager.instance.PlayerShootClip();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }
        else if (shootDirection == Vector2.up)
        {

            GameObject bullet = Instantiate(bulletPrefabs, upPoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = direction * bulletSpeed;
            AudioManager.instance.PlayerShootClip();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }
        else if (shootDirection == Vector2.down && isGround == false)
        {

            GameObject bullet = Instantiate(bulletPrefabs, downPoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = direction * bulletSpeed;
            AudioManager.instance.PlayerShootClip();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }
        ammo = ammo - 1;
        Debug.Log(ammo);
        if (ammo <= 0)
        {
            StartCoroutine(Reload());
        }

    }

    IEnumerator Reload()
    {
        isShooting = false;
        animator.SetTrigger("isReload");
        AudioManager.instance.Reload();

        yield return new WaitForSeconds(1.5f);
        yield return ammo = 10;
        isShooting = true;
    }

    public void PlayerThrowBoom()
    {

        if (Input.GetKeyDown(KeyCode.Q) && !Input.GetKey(KeyCode.S))
        {
            grenadeCount--;
            if (grenadeCount > 0)
            {
                animator.SetTrigger("isGrenade");
                GameObject boom = Instantiate(boomPrefabs, throwPoint.position, Quaternion.identity);
                Rigidbody2D rb = boom.GetComponent<Rigidbody2D>();

                Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
                Vector2 force = direction * throwForceX + Vector2.up * throwForceY;
                rb.AddForce(force, ForceMode2D.Impulse);

                rb.angularVelocity = spinForce;
            }
            else
            {
                grenadeCount = 0;
                animator.SetTrigger("isGrenade");
            }
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Q))
        {
            grenadeCount--;
            if (grenadeCount > 0)
            {
                animator.SetTrigger("isCrouchGrenade");
                GameObject boom = Instantiate(boomPrefabs, throwPoint.position, Quaternion.identity);
                Rigidbody2D rb = boom.GetComponent<Rigidbody2D>();

                Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
                Vector2 force = direction * throwForceX + Vector2.up * throwForceY;
                rb.AddForce(force, ForceMode2D.Impulse);

                rb.angularVelocity = spinForce;
            }
            else
            {
                grenadeCount = 0;
                animator.SetTrigger("isCrouchGrenade");
            }
        }

    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (shield.activeSelf && (other.CompareTag("EnemyBullet") || other.CompareTag("BossBullet")))
        {
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("BossBullet"))
        {
            playerHp = playerHp - 2;
            AudioManager.instance.GettingHit();
            CheckPlayerHpLife();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("EnemyBullet"))
        {
            playerHp = playerHp - 2;
            AudioManager.instance.GettingHit();
            CheckPlayerHpLife();
            Destroy(other.gameObject);
        }

        else if (other.CompareTag("BossTrigger"))
        {
            isMove = false;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            animator.SetBool("isWalk", false);
            playController.enabled = false;
            spwaningCritpt.isSpawning = false;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("BossBoom"))
        {
            playerHp = playerHp - 5;
            AudioManager.instance.GettingHit();
            CheckPlayerHpLife();
            Destroy(other.gameObject);
        }
    }

    public void SetTrue()
    {
        isMove = true;
        if (spwaningCritpt != null && !spwaningCritpt.Equals(null))
            spwaningCritpt.isSpawning = true;

        if (animator != null && !animator.Equals(null))
            animator.SetBool("isWalk", true);

        if (rb != null && !rb.Equals(null))
        {
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        }

        if (playController != null && !playController.Equals(null))
            playController.enabled = true;

    }

    public void CheckPlayerHpLife()
    {
        if (playerHp <= 0)
        {
            Instantiate(deathPrefabs, transform.position, Quaternion.identity);
            AudioManager.instance.PlayerDeath();
            Destroy(gameObject);
        }
    }


}
