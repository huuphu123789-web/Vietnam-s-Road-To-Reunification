using UnityEngine;
using System.Collections;

public class BossAttack : MonoBehaviour
{
    [Header("Bomb Settings")]
    public GameObject bombPrefab;
    public Transform firePoint;
    public float timeBetweenBombs = 0.2f;
    public float timeBetweenBombRounds = 5f;
    public int bombsPerRound = 4;

    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float timeBetweenBullets = 0.1f;
    public float timeBetweenShootRounds = 3f;
    public int bulletsPerRound = 6;

    void Start()
    {
        StartCoroutine(BombLoop());
        StartCoroutine(ShootLoop());
    }

    IEnumerator BombLoop()
    {
        while (true)
        {
            yield return StartCoroutine(DropBombs());
            yield return new WaitForSeconds(timeBetweenBombRounds);
        }
    }

    IEnumerator DropBombs()
    {
        for (int i = 0; i < bombsPerRound; i++)
        {
            Instantiate(bombPrefab, firePoint.position, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenBombs);
        }
    }

    IEnumerator ShootLoop()
    {
        while (true)
        {
            yield return StartCoroutine(ShootBullets());
            yield return new WaitForSeconds(timeBetweenShootRounds);
        }
    }

    IEnumerator ShootBullets()
    {
        for (int i = 0; i < bulletsPerRound; i++)
        {
            Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenBullets);
        }
    }
}