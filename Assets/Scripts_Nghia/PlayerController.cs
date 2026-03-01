using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float crouchSpeed = 3f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public Vector2 groundCheckSize = new Vector2(0.4f, 0.05f);
    public LayerMask groundLayer;

    [Header("References")]
    public Rigidbody2D rb;
    public GameObject top;
    public GameObject down;

    [Header("Animator")]
    public Animator topAnim;
    public Animator downAnim;

    [Header("Fire / Knife / Throw")]
    public Collider2D knifeHitBoxTOP;
    public Collider2D knifeHitBoxDOWN;

    [Header("Fire")]
    public float fireRate = 0.12f;

    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("FirePoint Offset")]
    public Vector2 standSideLocalPos = new Vector2(0.45f, 0.12f);
    public Vector2 standUpLocalPos = new Vector2(0f, 0.30f);
    public Vector2 crouchSideLocalPos = new Vector2(0.40f, -0.05f);

    [Header("Weapon")]
    public WeaponType currentWeapon = WeaponType.Pistol;

    public GameObject pistolBullet;
    public GameObject akBullet;

    public float pistolFireRate = 0.25f;
    public float akFireRate = 0.08f;

    [Header("Base Controllers (Pistol)")]
    public RuntimeAnimatorController pistolTopController;
    public RuntimeAnimatorController pistolDownController;

    [Header("Animator Controllers")]
    public AnimatorOverrideController akTopOverride;
    public AnimatorOverrideController akDownOverride;

    [Header("Throw Point")]
    public Transform throwPointTop;
    public Transform throwPointDown;

    public GameObject grenadePrefab;
    public float throwForce = 8f;

    bool isGrounded;
    bool isCrouch;
    bool isShootingHold;
    bool isDoingOnceAttack;
    bool isTopAttacking;
    bool isCrouchMoving;

    float fireTimer;
    float moveInput;

    float aimAngle = 0f;
    float aimTarget = 0f;
    public float aimSpeed = 90f;

    void Awake()
    {
        topAnim = top.GetComponent<Animator>();
        downAnim = down.GetComponent<Animator>();

        knifeHitBoxTOP.enabled = false;
        knifeHitBoxDOWN.enabled = false;
    }

    void Update()
    {
        CheckGround();
        HandleInput();

        UpdateAimSmooth();
        UpdateFirePoint();

        isCrouchMoving = isCrouch && Mathf.Abs(moveInput) > 0.1f;

        HandleAttack();
        UpdateAnimator();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleInput()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        isCrouch = (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && isGrounded;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isCrouch)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        isShootingHold = Input.GetMouseButton(0) || Input.GetKey(KeyCode.J);

        if (isShootingHold && !isCrouch)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                aimTarget = aimAngle < 45f ? 45f : 90f;
            else
                aimTarget = 0f;
        }
        else
            aimTarget = 0f;

        if (isCrouchMoving) return;

        if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.K)) && !isDoingOnceAttack)
            TriggerKnife();

        if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.L)) && !isDoingOnceAttack)
            TriggerThrow();
    }

    void HandleMovement()
    {
        if (isCrouch && (isTopAttacking || isDoingOnceAttack))
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        float speed = isCrouch ? crouchSpeed : moveSpeed;
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        if (moveInput != 0)
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1f, 1f);
    }

    void UpdateAimSmooth()
    {
        if (!isShootingHold)
            aimTarget = 0f;

        aimAngle = Mathf.MoveTowards(
            aimAngle,
            aimTarget,
            aimSpeed * Time.deltaTime
        );
    }

    Vector2 GetShootDir()
    {
        if (isCrouch)
            return new Vector2(transform.localScale.x, 0f);

        float rad = aimAngle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad) * transform.localScale.x, Mathf.Sin(rad));
    }

    void UpdateFirePoint()
    {
        if (!firePoint) return;

        if (isCrouch)
        {
            firePoint.localPosition = crouchSideLocalPos;
            return;
        }

        float t = Mathf.InverseLerp(0f, 90f, aimAngle);
        firePoint.localPosition = Vector2.Lerp(
            standSideLocalPos,
            standUpLocalPos,
            t
        );
    }

    void HandleAttack()
    {
        fireTimer -= Time.deltaTime;

        if (isCrouchMoving || isDoingOnceAttack) return;

        if (isShootingHold && fireTimer <= 0f)
        {
            fireTimer = fireRate;
            TriggerShoot();
        }

        if (!isShootingHold)
            fireTimer = 0f;
    }

    void TriggerShoot()
    {
        isTopAttacking = true;

        if (isCrouch) downAnim.SetTrigger("Shoot");
        else topAnim.SetTrigger("Shoot");

        FireBullet();
        Invoke(nameof(EndTopAttack), 0.05f);
    }

    void TriggerKnife()
    {
        isDoingOnceAttack = true;
        isTopAttacking = true;

        if (isCrouch) downAnim.SetTrigger("Knife");
        else topAnim.SetTrigger("Knife");

        (isCrouch ? knifeHitBoxDOWN : knifeHitBoxTOP).enabled = true;
        Invoke(nameof(EndKnife), 0.15f);
    }

    void EndKnife()
    {
        knifeHitBoxTOP.enabled = false;
        knifeHitBoxDOWN.enabled = false;

        isDoingOnceAttack = false;
        isTopAttacking = false;

        if (isShootingHold)
            fireTimer = 0f;
    }

    void TriggerThrow()
    {
        isDoingOnceAttack = true;
        isTopAttacking = true;

        if (isCrouch) downAnim.SetTrigger("Throw");
        else topAnim.SetTrigger("Throw");

        Invoke(nameof(ThrowGrenade), 0.12f);
        Invoke(nameof(EndThrow), 0.25f);
    }

    void EndThrow()
    {
        isDoingOnceAttack = false;
        isTopAttacking = false;

        if (isShootingHold)
            fireTimer = 0f;
    }

    void EndTopAttack()
    {
        if (!isDoingOnceAttack)
            isTopAttacking = false;
    }

    void UpdateAnimator()
    {
        bool isMoving = Mathf.Abs(moveInput) > 0.1f;
        top.SetActive(!isCrouch);

        downAnim.SetBool("isGrounded", isGrounded);
        downAnim.SetBool("isCrouch", isCrouch);
        downAnim.SetBool("isMoving", isMoving);

        if (!isCrouch)
        {
            topAnim.SetFloat("AimAngle", aimAngle);
            topAnim.SetBool("IsShooting", isTopAttacking);

            topAnim.SetBool("isGrounded", !isTopAttacking && isGrounded);
            topAnim.SetBool("isMoving", !isTopAttacking && isMoving);
        }
    }

    public void FireBullet()
    {
        if (!firePoint) return;

        GameObject prefab = currentWeapon == WeaponType.AK ? akBullet : pistolBullet;
        GameObject bullet = Instantiate(prefab, firePoint.position, Quaternion.identity);

        bullet.GetComponent<Bullet>().Init(GetShootDir());
    }

    public void SwitchWeapon(WeaponType newWeapon)
    {
        currentWeapon = newWeapon;

        if (currentWeapon == WeaponType.AK)
        {
            topAnim.runtimeAnimatorController = akTopOverride;
            downAnim.runtimeAnimatorController = akDownOverride;
            fireRate = akFireRate;
        }
        else
        {
            topAnim.runtimeAnimatorController = pistolTopController;
            downAnim.runtimeAnimatorController = pistolDownController;
            fireRate = pistolFireRate;
        }

        fireTimer = fireRate;
        isTopAttacking = false;

        topAnim.Rebind(); topAnim.Update(0f);
        downAnim.Rebind(); downAnim.Update(0f);
    }

    Transform GetThrowPoint()
    {
        return isCrouch ? throwPointDown : throwPointTop;
    }

    Vector2 GetThrowDir()
    {
        if (isCrouch)
            return new Vector2(transform.localScale.x, 0f);

        float throwAngle = 45f;
        float rad = throwAngle * Mathf.Deg2Rad;

        return new Vector2(
            Mathf.Cos(rad) * transform.localScale.x,
            Mathf.Sin(rad)
        );
    }


    void ThrowGrenade()
    {
        Transform tp = GetThrowPoint();
        if (!tp) return;

        GameObject grenade = Instantiate(grenadePrefab, tp.position, Quaternion.identity);
        Rigidbody2D grb = grenade.GetComponent<Rigidbody2D>();
        if (!grb) return;

        grb.linearVelocity = GetThrowDir().normalized * throwForce;
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapBox(
            groundCheck.position,
            groundCheckSize,
            0f,
            groundLayer
        );
    }

    void OnDrawGizmosSelected()
    {
        if (!groundCheck) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
    }
}
