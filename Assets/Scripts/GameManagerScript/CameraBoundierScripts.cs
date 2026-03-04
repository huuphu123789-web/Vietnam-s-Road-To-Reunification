using UnityEngine;

public class CameraBoundierScripts : MonoBehaviour
{
    public static CameraBoundierScripts instance;
        void Start()
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
