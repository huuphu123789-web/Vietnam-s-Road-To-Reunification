using System.Linq;

using NUnit.Framework.Constraints;
using UnityEngine;

public class BossBulletCript : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Rigidbody2D rb;

    [SerializeField] float bulletSpeed = 5f;

    Vector2 shootDirection;
    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        DestroyBullet();
        FindDirection();

    }

    public void FindDirection()
    {
        PlayController found = FindFirstObjectByType<PlayController>();
        if (found != null)
        {
            player = found.transform;
        }
        else
        {

        }

        if (rb.position.x > player.position.x && shootDirection != Vector2.right)
        {
            shootDirection = Vector2.left;
            rb.linearVelocity = shootDirection * bulletSpeed;
        }
        else if (rb.position.x < player.position.x && shootDirection != Vector2.left)
        {
            shootDirection = Vector2.right;
            rb.linearVelocity = shootDirection * bulletSpeed;
        }

    }


    public void DestroyBullet()
    {
        Destroy(gameObject, 3);
    }


}
