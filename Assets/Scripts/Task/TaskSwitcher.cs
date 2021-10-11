using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSwitcher : MonoBehaviour
{
    [SerializeField]
    private TaskList _taskList;
    [SerializeField]
    private TaskVisualizator _taskVisualizator;
    [SerializeField]
    private TaskActivator _taskActivator;
    public void SwitchTask(TrailerTask task)
    {
        _taskList.SetCurrentTask(task);
        _taskVisualizator.VisualizateCurrentTask(task);
        _taskActivator.DisableOtherTask(task);

    }
}
