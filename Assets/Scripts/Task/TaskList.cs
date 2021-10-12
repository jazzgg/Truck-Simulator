using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TaskList : MonoBehaviour 
{
    [SerializeField]
    private GameObject[] _childList; 
    private Dictionary<TrailerTask, TaskVisusalization> _tasks;
    [SerializeField]
    private TaskActivator _taskActivator;
    private KeyValuePair<TrailerTask, TaskVisusalization> _currentTask;

    private void Start()
    {
        _tasks = new Dictionary<TrailerTask, TaskVisusalization>();
        
        foreach (var child in _childList)
        {
            _tasks.Add(child.GetComponent<TrailerTask>(), child.GetComponent<TaskVisusalization>());
        } 

        _taskActivator.OnTaskCompleted += RemoveCurrentTask;
    }
    private void RemoveCurrentTask()
    {
        _tasks.Remove(_currentTask.Key);
    }

    public void SetCurrentTask(KeyValuePair<TrailerTask, TaskVisusalization> task)
    {
        _currentTask = task;
    }
    public KeyValuePair<TrailerTask, TaskVisusalization> GetCurrentTask()
    {
        return _currentTask;
    }
    public Dictionary<TrailerTask, TaskVisusalization> GetTasks()
    {
        return _tasks;
    }
    private void OnDestroy()
    {
        _taskActivator.OnTaskCompleted -= RemoveCurrentTask;
    }
}
 