using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSwitcher : MonoBehaviour
{
    private TaskList _taskList;
    private TaskVisualizator _taskVisualizator;
    private TaskActivator _taskActivator;

    private void Start()
    {
        _taskList = GetComponent<TaskList>();
        _taskVisualizator = GetComponent<TaskVisualizator>(); 
        _taskActivator = GetComponent<TaskActivator>();

    }
    public void SwitchTask(TrailerTask task)
    {
        _taskList.SetCurrentTask(task);
        _taskVisualizator.VisualizeSelectedTask();
        _taskActivator.ActivateCurrentTask();
    }
}
