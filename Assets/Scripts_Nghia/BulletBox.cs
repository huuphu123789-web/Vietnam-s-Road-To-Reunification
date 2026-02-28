using UnityEngine;

public class BulletBox : MonoBehaviour
{
    public WeaponType weaponType = WeaponType.AK;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (!player) return;

        player.SwitchWeapon(weaponType);

        Destroy(gameObject);
    }
}
