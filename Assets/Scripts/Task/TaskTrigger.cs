using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _tasks;

    private void Start()
    {
        foreach (var task in _tasks)
        {
            task.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        foreach (var task in _tasks)
        {
            task.SetActive(true);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        foreach (var task in _tasks)
        {
            task.SetActive(false);
        }
    }
}
