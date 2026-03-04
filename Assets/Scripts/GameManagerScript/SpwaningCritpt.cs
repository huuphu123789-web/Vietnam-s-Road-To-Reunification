using System.Collections;
using Unity.Cinemachine;
using UnityEngine;


public class SpwaningCritpt : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
  

   [SerializeField] private GameObject gameOverPanel;

   [SerializeField] public GameObject victoryPanel;

    public Vector3 spawnPoin;
    public static SpwaningCritpt instance;
    public GameObject[] enemies;
    public bool isSpawning;
    // Vector3 bottomLeft  = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
    // Vector3 bottomRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane));
    // Vector3 topLeft     = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.nearClipPlane));
    // Vector3 topRight    = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
    // Vector3 center      = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane));
    private void Awake()
    {
        isSpawning=true;
         if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
    if (!isSpawning) return;

    if (Camera.main == null) return;
    if (enemies == null || enemies.Length == 0) return;

    float halfHeight = Camera.main.orthographicSize;
    float halfWidth = halfHeight * Camera.main.aspect;

    Vector3 camPos = Camera.main.transform.position;

    Vector3[] spawnPoints = new Vector3[3];

    spawnPoints[0] = new Vector3(
        Random.Range(camPos.x - halfWidth, camPos.x + halfWidth),
        camPos.y + halfHeight + 1f,
        0
    );


    spawnPoints[1] = new Vector3(
    camPos.x - halfWidth - 1f,
    camPos.y + halfHeight * 0.5f,   
    0
    );


    spawnPoints[2] = new Vector3(
    camPos.x + halfWidth + 1f,
    camPos.y + halfHeight * 0.5f,
    0
    );

    int randomIndex = Random.Range(0, spawnPoints.Length);
    Vector3 spawnPos = spawnPoints[randomIndex];

    int randomEnemy = Random.Range(0, enemies.Length);
    Instantiate(enemies[randomEnemy], spawnPos, Quaternion.identity);
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
        if(PlayerController.instance.playerHp<=0)
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
            StartCoroutine(turnOffvictoryPanel());

   }
    IEnumerator turnOffvictoryPanel()
    {
        yield return new WaitForSeconds(2);
        
            victoryPanel.SetActive(false);
        
    }
}


