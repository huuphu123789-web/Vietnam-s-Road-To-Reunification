using UnityEngine;

public class BazooBulletScript : MonoBehaviour
{
    [SerializeField] GameObject explodePre;
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Instantiate(explodePre,transform.position,Quaternion.identity);
            PlayerController.instance.playerHp -=5;
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject,5);
        }
    }
}
