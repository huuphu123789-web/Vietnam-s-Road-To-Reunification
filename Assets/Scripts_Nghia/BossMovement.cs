using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossMovement : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 3f;
    public int bossHp;
    public GameObject bossDeathPre;
    private bool goingToB = true;
    public Vector3 offset;
    public float timetoChangeMap;
    void Update()
    {
       
    {
    Transform target = goingToB ? pointB : pointA;

    transform.position = Vector3.MoveTowards(
        transform.position,
        target.position,
        moveSpeed * Time.deltaTime
    );

    if (Vector3.Distance(transform.position, target.position) < 0.1f)
    {
        goingToB = !goingToB;
    }
    }
        Debug.Log(Vector3.Distance(transform.position, pointB.position));
        CheckBossDeath();
    }
    public void CheckBossDeath()
{
    if (bossHp <= 0)
    {
        Instantiate(bossDeathPre, transform.position + offset, Quaternion.identity);
        SpwaningCritpt.instance.Victory();
        AudioManager.instance.StopBackgroundMusic();
        SpwaningCritpt.instance.DestroyAllEnemies();
        SpwaningCritpt.instance.DestroyAllEnemyBullet();
        SpwaningCritpt.instance.DestroyAllEnemyKnife();
        SpwaningCritpt.instance.StartCoroutine(LoadMap2());
        gameObject.SetActive(false);
    }
}
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bullet"))
        {
            AudioManager.instance.BossHitClip();
            bossHp-=3;
            Destroy(other.gameObject);
            CheckBossDeath();
        }
        else if (other.CompareTag("Grenade"))
        {
            bossHp -=5;
            CheckBossDeath();
        }
    }

    IEnumerator LoadMap2()
    {
        AudioManager.instance.StopBackgroundMusic();
        yield return new WaitForSeconds(timetoChangeMap);
        SceneManager.LoadScene("Map-2");
    }
}