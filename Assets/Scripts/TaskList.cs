using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TaskList : MonoBehaviour
{
    public event Action OnSetTask;

    private TrailerTask _currentTask;
    private TrailerTask _previousTask;

    private TrailerTask[] _tasks => GetComponentsInChildren<TrailerTask>();

    public void SetCurrentTask(TrailerTask task)
    {
        _previousTask = _currentTask;
        _currentTask = task;

        OnSetTask?.Invoke();
    }
    public TrailerTask GetPreviousTask()
    {
        return _previousTask;
    }
    public TrailerTask GetCurrentTask()
    {
        return _currentTask;
    }
    public TrailerTask[] GetTasks()
    {
        return _tasks;
    }

}
 