using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float timetoChangeMap;
    void Start()
    {
        instance = this;
    }

    IEnumerator LoadMap2()
    {
        yield return new WaitForSeconds(timetoChangeMap);
        SceneManager.LoadScene("Map-2");
    }
}
