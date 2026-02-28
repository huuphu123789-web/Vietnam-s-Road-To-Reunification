using UnityEngine;

public class KnifeDamage : MonoBehaviour
{
    public int damage = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.SendMessage(
                "TakeDamage",
                damage,
                SendMessageOptions.DontRequireReceiver
            );
        }
    }
}
