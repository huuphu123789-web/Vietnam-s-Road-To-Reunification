using UnityEngine;
using UnityEngine.Splines;
using System.Collections.Generic;

[RequireComponent(typeof(EdgeCollider2D))]
public class SplineToEdgeCollider2D : MonoBehaviour
{
    public SplineContainer splineContainer;

    [Range(10, 2000)]
    public int resolution = 100;

    void Awake()
    {
        Bake();
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (!Application.isPlaying)
            Bake();
    }
#endif

    public void Bake()
    {
        if (splineContainer == null) return;

        EdgeCollider2D edge = GetComponent<EdgeCollider2D>();
        Spline spline = splineContainer.Spline;

        List<Vector2> points = new List<Vector2>(resolution + 1);

        for (int i = 0; i <= resolution; i++)
        {
            float t = i / (float)resolution;

            // Evaluate xuyên suốt TẤT CẢ các đoạn spline (0 → 10 cũng OK)
            Vector3 worldPos = spline.EvaluatePosition(t);

            // World → Local (EdgeCollider cần local)
            Vector3 localPos = transform.InverseTransformPoint(worldPos);

            points.Add(new Vector2(localPos.x, localPos.y));
        }

        edge.points = points.ToArray();
    }
}