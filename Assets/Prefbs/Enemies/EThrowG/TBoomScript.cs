using System;
using UnityEngine;

public class TBoomScript : MonoBehaviour
{
    [SerializeField] private  int damge;
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    [SerializeField] private GameObject explodePrefabs;
    public Vector3 offset = new Vector3(0,0.5f,0);
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator =GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Explosion();
            PlayController.instance.playerHp -= 5;
        }

        else if (other.CompareTag("Ground"))
        {
            Explosion();
        }
    }

    void Explosion()
    {
        Instantiate(explodePrefabs,transform.position+ offset,Quaternion.identity);
        Destroy(gameObject);
    }
    
}
