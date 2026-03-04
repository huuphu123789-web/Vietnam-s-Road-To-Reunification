using UnityEngine;

public class LauncherBullet : MonoBehaviour
{
    [SerializeField] GameObject exploPre;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            AudioManager.instance.GettingHit();
            PlayerController.instance.playerHp -=5;
            Destroy(gameObject);

        }
        else if(other.CompareTag("Ground"))
        {
            Instantiate(exploPre,transform.position,Quaternion.identity);
        }
        
    }
}
