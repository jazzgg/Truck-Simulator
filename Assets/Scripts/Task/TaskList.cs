using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TaskList : MonoBehaviour 
{ 
    [SerializeField]
    private List<TrailerTask> _tasks;
    [SerializeField]
    private TaskActivator _taskActivator;

    private TrailerTask _currentTask;

    private void Start()
    {
        _taskActivator.OnTaskCompleted += RemoveCurrentTask;

        _currentTask = _tasks[0];
    }
    private void RemoveCurrentTask()
    {
        _tasks.Remove(_currentTask);
    }

    public void SetCurrentTask(TrailerTask task)
    {
        _currentTask = task;
    }
    public TrailerTask GetCurrentTask()
    {
        return _currentTask;
    }
    public List<TrailerTask> GetTasks()
    {
        return _tasks;
    }
    private void OnDestroy()
    {
        _taskActivator.OnTaskCompleted -= RemoveCurrentTask;
    }
}
 