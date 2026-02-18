
using UnityEngine;

public class EndMapTrigger : MonoBehaviour
{
    [SerializeField] public GameObject bossPrefab;
    [SerializeField]  public GameObject enemy ;
    public static EndMapTrigger intance;
    void Awake()
    {
        
    }
    void Update()
    {
       
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnBoss();
            AudioManager.instance.BossFight();
            Destroy(gameObject);
        }
    }
    void SpawnBoss()
    {
   
        AudioManager.instance.BossFight();
        SpwaningCritpt.instance.DestroyAllEnemies();
        SpwaningCritpt.instance.DestroyAllEnemyKnife();
        Vector3 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.nearClipPlane));
        spawnPos.z = 0f;
        GameObject boss = Instantiate(bossPrefab, spawnPos, Quaternion.identity);
       
    }
}