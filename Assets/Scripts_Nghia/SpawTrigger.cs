using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public GameObject bossPrefab;   // Prefab boss
    public Transform spawnPoint;    // Điểm spawn
    public bool spawnOnce = true;   // Chỉ spawn 1 lần

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