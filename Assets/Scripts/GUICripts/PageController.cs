using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class PageController : MonoBehaviour
{
    [SerializeField] List<GameObject> list = new List<GameObject>();
    int index = 0;
    void Awake()
    {
        AudioManager.instance.StopBackgroundMusic();
    }
    public void PreBtnClick()
    {
        if (index == 0)
        {
            list[index].SetActive(false);
            index = list.Count - 1;
            list[index].SetActive(true);
        }
        else
        {
            list[index].SetActive(false);
            index--;
            list[index].SetActive(true);
        }
    }

    public void NexBtnClick()
    {
        if (index == list.Count - 1)
        {
            list[index].SetActive(false);
            index = 0;
            list[index].SetActive(true);
        }
        else
        {
            list[index].SetActive(false);
            index++;
            list[index].SetActive(true);
        }
    }
    
    public void ExitBtnCkick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
