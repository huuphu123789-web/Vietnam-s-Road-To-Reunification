using UnityEngine;

public class ExplosionSript : MonoBehaviour
{
    public GameObject exploPrefabs;
    void Update()
    {
        Destroy(exploPrefabs,1f);
    }
}
