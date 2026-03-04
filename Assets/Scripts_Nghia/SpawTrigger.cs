using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public GameObject bossPrefab;   
    public Transform spawnPoint;    
    public bool spawnOnce = true;   

    private bool hasSpawned = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (spawnOnce && hasSpawned) return;

            Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);

            hasSpawned = true;
        }
    }
}