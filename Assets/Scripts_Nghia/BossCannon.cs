using UnityEngine;

public class BossCannon : MonoBehaviour
{
    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Effect")]
    public GameObject muzzleFlashPrefab;
    public float muzzleFlashTime = 1f;

    [Header("Fire Rate")]
    public float fireRate = 5f;
    private float timer;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogWarning("Missing Bullet Prefab or FirePoint!");
            return;
        }

        if (anim != null)
            anim.SetTrigger("Shoot");

        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        if (muzzleFlashPrefab != null)
        {
            GameObject flash = Instantiate(
                muzzleFlashPrefab,
                firePoint.position,
                firePoint.rotation
            );

            Destroy(flash, muzzleFlashTime);
        }
    }
}