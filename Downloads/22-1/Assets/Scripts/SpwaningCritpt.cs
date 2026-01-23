using System.Collections;
using Unity.Cinemachine;
using UnityEngine;


public class SpwaningCritpt : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
  
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject enemyKnife;
   [SerializeField] private GameObject gameOverPanel;

   [SerializeField] public GameObject victoryPanel;

    public Vector3 spawnPoin;
    public static SpwaningCritpt instance;
    public bool isSpawning;
    // Vector3 bottomLeft  = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
    // Vector3 bottomRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane));
    // Vector3 topLeft     = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.nearClipPlane));
    // Vector3 topRight    = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
    // Vector3 center      = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane));
    private void Awake()
    {
        isSpawning=true;
        instance=this;
        spawnPoin= transform.position;
    }
    void Start()
    {
        
        InvokeRepeating("SpawnEnemy",1f,2f);
        SpawningPlayerOneTime();
    }
    void Update()
    {
       CheckGameOver();
    }
    void SpawnEnemy()
    {
        if(isSpawning == true)
        {

        // 5 điểm viewport
        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = halfHeight * Camera.main.aspect;
        
          Vector3[] spawnPoints = new Vector3[2];
        // spawnPoints[0] = new Vector3(Camera.main.transform.position.x - halfWidth + offset,
        //                              Camera.main.transform.position.y - halfHeight + offset, 0); // trái dưới ngoài
        // spawnPoints[1] = new Vector3(Camera.main.transform.position.x + halfWidth + offset,
        //                              Camera.main.transform.position.y - halfHeight + offset, 0); // phải dưới ngoài
        spawnPoints[0] = new Vector3(Camera.main.transform.position.x - halfWidth ,
                                     Camera.main.transform.position.y  , 0); 
                                     // trái trên ngoài
        spawnPoints[1] = new Vector3(Camera.main.transform.position.x + halfWidth,
                                     Camera.main.transform.position.y , 0);
                                      // phải trên ngoài
        

        // Chọn ngẫu nhiên 1 điểm
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Vector3 spawnPos = spawnPoints[randomIndex];
        GameObject[] enemyies = { enemyPrefab, enemyKnife };

        int randomEnemy=Random.Range(0,enemyies.Length);
        // Spawn enemy
        Instantiate(enemyies[randomEnemy], spawnPos, Quaternion.identity);
        }
        

    }
    
    public void SpawningPlayer()
    {
        StartCoroutine(SpawnPlayer());
    }
   IEnumerator SpawnPlayer()
    {   
        yield return new WaitForSeconds(1);
        
       
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0.5f ,1,10f ));
        GameObject player = Instantiate (playerPrefab, bottomLeft, Quaternion.identity);
        
        CinemachineCamera vcam = Object.FindFirstObjectByType<CinemachineCamera>();
        if (vcam != null)
        {
            vcam.transform.position = player.transform.position;
            vcam.Follow = player.transform;
            vcam.LookAt = player.transform;
        }
        else
        {
            Debug.LogWarning("Không tìm thấy Cinemachine Virtual Camera!");
        }
    }

    void SpawningPlayerOneTime()
    {
        
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0.5f ,1,10f ));
        GameObject player = Instantiate (playerPrefab, bottomLeft, Quaternion.identity);
        
        CinemachineCamera vcam = Object.FindFirstObjectByType<CinemachineCamera>();
        if (vcam != null)
        {
            vcam.transform.position = player.transform.position;
            vcam.Follow = player.transform;
            vcam.LookAt = player.transform;
        }
        else
        {
            Debug.LogWarning("Không tìm thấy Cinemachine Virtual Camera!");
        } 
    }

   
    public void DestroyAllEnemies()
{
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    foreach (GameObject enemy in enemies)
    {
        Destroy(enemy);
    }
}

public void DestroyAllEnemyBullet()
    {
        GameObject[] bullet = GameObject.FindGameObjectsWithTag ("EnemyBullet");

        foreach(GameObject bullet2 in bullet)
        {
            Destroy(bullet2);
        }
    }
public void DestroyAllEnemyKnife()
    {
        GameObject[] knife = GameObject.FindGameObjectsWithTag ("EnemyKnife");

        foreach(GameObject knifes in knife)
        {
            Destroy(knifes);
        }
    }
public void CheckGameOver()
    {
        if(PlayController.instance.playerHp<=0)
        {   
            gameOverPanel.SetActive(true);
            AudioManager.instance.StopPlaySceneClip();
            
        }
    }
    public void Victory()
    {
            victoryPanel.SetActive(true);
            AudioManager.instance.StopPlaySceneClip();
            AudioManager.instance.VictorySound();
            isSpawning=false;
   }
}


