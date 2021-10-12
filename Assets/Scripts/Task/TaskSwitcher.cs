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

    private TrailerTask _task;
    private TaskVisusalization _taskVis;
    public void SwitchTask()
    {
        var keyValuePair = new KeyValuePair<TrailerTask, TaskVisusalization>(_task, _taskVis);

        _taskList.SetCurrentTask(keyValuePair);
        _taskVisualizator.VisualizateCurrentTask(keyValuePair);
        _taskActivator.DisableOtherTask(keyValuePair);
    }
    public void SetTask(TrailerTask task)
    {
        _task = task;
    }
    public void SetTaskVis(TaskVisusalization taskVis)
    {
        _taskVis = taskVis;
    }
}
