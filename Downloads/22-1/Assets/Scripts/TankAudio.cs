using UnityEngine;

public class TankAudio : MonoBehaviour
{
    [SerializeField] AudioSource tankAudio;
    [SerializeField] AudioClip tankAudioClip;

    public static TankAudio instance;
    void Awake()
    {
         tankAudio.PlayOneShot(tankAudioClip);
    }
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
}