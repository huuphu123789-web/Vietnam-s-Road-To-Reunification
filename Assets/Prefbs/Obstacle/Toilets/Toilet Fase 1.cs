

using UnityEngine;

public class ToiletFase1 : MonoBehaviour
{
    [SerializeField] private int hp = 5;
    [SerializeField] private GameObject toiletFase2;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private Vector3 offset;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator =GetComponent<Animator>();
        offset = new Vector3(0,-1,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bullet"))
        {   
            Destroy(other.gameObject);
            animator.SetTrigger("isHit");  
            hp=hp-1;
            Debug.Log(hp);
            if(hp <=0)
            {
                Instantiate(toiletFase2,transform.position + offset,Quaternion.identity);
                Destroy(gameObject);
            }
        }

        else if(other.CompareTag("Grenade"))
        {
            Destroy(other.gameObject);
            Instantiate(toiletFase2,transform.position + offset,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
