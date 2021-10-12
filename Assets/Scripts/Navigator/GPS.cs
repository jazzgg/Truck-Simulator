using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InsaneSystems.RoadNavigator;

public class GPS : MonoBehaviour
{
    [SerializeField]
    private Navigator _navigator;

    public void SetNewTarget(Vector3 trailerFinishPoint)
    {
        _navigator.SetTargetPoint(trailerFinishPoint);
    }
}
