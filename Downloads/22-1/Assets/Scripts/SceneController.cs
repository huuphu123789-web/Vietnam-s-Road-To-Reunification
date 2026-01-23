using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator transitionAnim;
    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        transitionAnim.SetTrigger("StartTrans");
    }

    public void TransToPlayScene()
    {
        transitionAnim.SetTrigger("StartTrans");
    }

    public void TransToMainMenu()
    {
        transitionAnim.SetTrigger("EndTrans");
    }


}
