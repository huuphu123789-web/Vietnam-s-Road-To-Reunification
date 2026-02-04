using System.Collections;
using UnityEngine;

public class CarsScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private int hp = 11;
    [SerializeField] private GameObject carDeathPrefabs;
    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
        animator.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bullet"))
        {   
            Destroy(other.gameObject);
            animator.SetTrigger("isHit");
            hp--;
       DestroyCars();
        }

    }

    public void DestroyCars()
    {
        if(hp<=0)
        {
            Instantiate(carDeathPrefabs,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
