using UnityEngine;

public class CinemachineScripts : MonoBehaviour
{
    public static CinemachineScripts instance;

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
    }
}
