using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    private void Start()
    {
        var navigator = FindObjectOfType<InsaneSystems.RoadNavigator.Navigator>();

        navigator.SetTargetPoint(_target.position);
    }
}
