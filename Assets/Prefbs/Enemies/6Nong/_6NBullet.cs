using UnityEngine;

public class _6NBullet : MonoBehaviour
{
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayController.instance.playerHp -=5;
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject,5);
        }
    }
}
