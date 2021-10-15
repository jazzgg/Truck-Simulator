using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InsaneSystems.RoadNavigator;
using System;

public class GPS : MonoBehaviour
{
    [SerializeField]
    private Navigator _navigator;

    public void SetNewTarget(Vector3 target)
    {
        _navigator.SetTargetPoint(target);
    }
    public void StopTargetFollow()
    {
        _navigator.StopNavigation();
    }
}
