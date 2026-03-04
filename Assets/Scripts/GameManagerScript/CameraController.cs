using Unity.Cinemachine;

using UnityEngine;

public class CameraController : MonoBehaviour
{   
public Transform player;

 public float smoothSpeed = 0.125f;

    void Awake()
    {  
        if (AudioManager.instance != null) 
        { 
            AudioManager.instance.StopBackgroundMusic(); 
            AudioManager.instance.PlaySceneBackGround();
            AudioManager.instance.Mission1();
        }
         else 
         { Debug.LogWarning("Không tìm thấy AudioManager khi Awake."); }
        
    }
    void Update()
{
    PlayerController found = GameObject.FindAnyObjectByType<PlayerController>(); 
    if (found != null) 
    {
        player = found.transform;
    }
    // Lấy kích thước camera
    float halfHeight = Camera.main.orthographicSize;
    float halfWidth = halfHeight * Camera.main.aspect;

    // Tính biên camera
    float camX = Camera.main.transform.position.x;
    float camY = Camera.main.transform.position.y;

    float minX = camX - halfWidth  +1;
    float maxX = camX + halfWidth - 1;
    float minY = camY - halfHeight;
    float maxY = camY + halfHeight;

    // Giới hạn Player
   if (player != null && !player.Equals(null))
{   
    
    Vector3 playerPos = player.position;
    playerPos.x = Mathf.Clamp(playerPos.x, minX, maxX);
    playerPos.y = Mathf.Clamp(playerPos.y, minY, maxY);
    player.position = playerPos;
}
}

 
}



