using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class LauncherScript : MonoBehaviour
{
   [SerializeField] private Rigidbody2D rb;
   [SerializeField] private Animator animator;
   [SerializeField] private Transform player,launchPoint;

   [SerializeField] private GameObject rocketPre,deadPre,deadbyGPre;
   

    float distanceformPlayer;
    public float lineofSite,launchRange,launchForceX,launchForceY;
    public float launchRate,reloadLaunch;
    public Vector3 offset;

    int rocketCount;
    int maxRocket;
    bool isLaunch=false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player= GameObject.FindGameObjectWithTag("Player").transform;
        FindPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        distanceformPlayer=Vector2.Distance(player.transform.position,transform.position);
        FindPlayer();
        DoLaunch();
    }
    IEnumerator Launch()
    {
            isLaunch = true;
    animator.SetBool("isLaunch", true);

    rocketCount = 0;
    maxRocket=0;
    while (rocketCount < 6)
    {
        float shootDir = Mathf.Sign(player.position.x - transform.position.x);
        Vector2 launchDir = new Vector2(shootDir * launchForceX, launchForceY);

        GameObject rocket = Instantiate(rocketPre, launchPoint.position, Quaternion.identity);
        rocket.GetComponent<Rigidbody2D>().AddForce(launchDir, ForceMode2D.Impulse);

        rocketCount++;
        maxRocket++;
        
        yield return new WaitForSeconds(launchRate);
    if(maxRocket == 6)
        {
           animator.SetBool("isLaunch", false);
             yield return new WaitForSeconds(reloadLaunch);
        }
    }
    isLaunch=false;
    }
    public void DoLaunch()
    {
        if(distanceformPlayer < lineofSite && distanceformPlayer > launchRange)
        {
            if(!isLaunch)
        {
            StartCoroutine(Launch());
        }
        }
        
    }
    public void FindPlayer()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (!player) return;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bullet"))
        {
            Instantiate(deadPre,transform.position + offset,Quaternion.identity);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Grenade"))
        {
            Instantiate(deadbyGPre,transform.position,Quaternion.identity);
            Destroy(gameObject);
        
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,lineofSite);
        Gizmos.DrawWireSphere(transform.position,launchRange);
    }
}

