using UnityEngine;

public class ToiletScareSript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.instance.EnemySuprise();
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame

}
