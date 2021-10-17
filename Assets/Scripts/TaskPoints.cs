using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPoints : MonoBehaviour
{
    [SerializeField]
    private Transform[] _taskPoints;

    public Transform GetRandomPoint()
    {
        return _taskPoints[Random.Range(0, _taskPoints.Length)];
    }
    public float GetDistance(Vector3 point1, Vector3 point2)
    {
        return Vector3.Distance(point1, point2);
    }
}
