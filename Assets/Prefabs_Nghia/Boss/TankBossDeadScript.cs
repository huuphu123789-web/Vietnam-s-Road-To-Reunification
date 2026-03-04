using UnityEngine;

public class TankBossDeadScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject,4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
