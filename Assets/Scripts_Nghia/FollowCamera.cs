using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    [Header("Smooth")]
    public float smoothTime = 0.15f;

    [Header("Clamp")]
    public Vector2 minPos;
    public Vector2 maxPos;

    [Header("Pixel")]
    public float pixelsPerUnit = 16f;

    Camera cam;
    Vector3 velocity = Vector3.zero;
    float camHeight;
    float camWidth;

    void Start()
    {
        cam = Camera.main;
        camHeight = cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
    }

    void LateUpdate()
    {
        if (!target) return;

        Vector3 targetPos = target.position + offset;

        targetPos.x = Mathf.Clamp(
            targetPos.x,
            minPos.x + camWidth,
            maxPos.x - camWidth
        );

        targetPos.y = Mathf.Clamp(
            targetPos.y,
            minPos.y + camHeight,
            maxPos.y - camHeight
        );

        targetPos.z = transform.position.z;

        Vector3 smoothPos = Vector3.SmoothDamp(
            transform.position,
            targetPos,
            ref velocity,
            smoothTime
        );

        smoothPos.x = Mathf.Round(smoothPos.x * pixelsPerUnit) / pixelsPerUnit;

        transform.position = smoothPos;
    }
}
