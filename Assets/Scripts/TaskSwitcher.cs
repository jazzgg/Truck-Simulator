using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSwitcher : MonoBehaviour
{
    private TaskList _taskList => GetComponent<TaskList>();
    private TaskVisualizator _taskVisualizator => GetComponent<TaskVisualizator>();
    private TaskActivator _taskActivator => GetComponent<TaskActivator>();

    public void SwitchTask(TrailerTask task)
    {
        _taskList.SetCurrentTask(task);
        _taskVisualizator.VisualizeSelectedTask();
        _taskActivator.ActivateCurrentTask();
    }
}
