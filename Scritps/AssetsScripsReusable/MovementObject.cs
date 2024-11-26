using UnityEngine;
using System.Collections.Generic;

public class MovementObject : MonoBehaviour
{
    [SerializeField] private float speed, pauseTime;
    [SerializeField] private List<Transform> points;
    private readonly List<Vector2> pointPositions = new List<Vector2>();
    private int currentPoint;
    private float pauseTimer;
    private bool isPaused;

    private void Start()
    {
        if (points == null || points.Count == 0)
            foreach (Transform child in transform) points.Add(child);

        foreach (Transform point in points)
        {
            if (point != null)
            {
                pointPositions.Add(point.position);
                point.gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (pointPositions == null || pointPositions.Count == 0) return;
        if (isPaused)
        {
            if ((pauseTimer += Time.deltaTime) >= pauseTime)
            {
                isPaused = false;
                pauseTimer = 0f;
                currentPoint = (currentPoint + 1) % pointPositions.Count;
            }
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, pointPositions[currentPoint], speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, pointPositions[currentPoint]) < 0.1f) isPaused = true;
    }

    private void OnDrawGizmos()
    {
        if (points == null || points.Count == 0) return;
        foreach (Transform point in points)
            if (point != null) Gizmos.DrawWireSphere(point.position, 0.3f);
        for (int i = 0; i < points.Count - 1; i++)
            if (points[i] != null && points[i + 1] != null)
                Gizmos.DrawLine(points[i].position, points[i + 1].position);
    }
}