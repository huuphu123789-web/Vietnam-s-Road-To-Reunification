using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class BulletSript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    
     public static BulletSript instance;

    void Update()
    {
        rb=GetComponent<Rigidbody2D>();
        Destroy(gameObject,2);
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
       
         if(other.CompareTag("Boss"))
        {
           AudioManager.instance.BossHitClip(); 
           Destroy(gameObject);
        }
       
    }

  
}
