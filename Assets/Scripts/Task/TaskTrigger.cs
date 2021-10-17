using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject _taskWindow;

    private void Start()
    {
        _taskWindow.SetActive(false);    
    }
    private void OnTriggerEnter(Collider other)
    {
        _taskWindow.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        _taskWindow.SetActive(false);
    }
}
