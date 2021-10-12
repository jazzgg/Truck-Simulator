using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TaskActivator : MonoBehaviour
{
    public event Action OnTaskCompleted;

    private TaskList _taskList;

    private void Start()
    {
        _taskList = GetComponent<TaskList>();

        foreach (var task in _taskList.GetTasks())
        {
            task.Key.GetTrailer().OnTrailerAttached += ChangeTaskStage;
            task.Key.GetTrailer().OnTrailerDetached += ChangeTaskStage;
        }

    }
    public void DisableOtherTask(KeyValuePair<TrailerTask, TaskVisusalization> task)
    {
        foreach (var taskk in _taskList.GetTasks())
        {
            if (taskk.Key == task.Key)
            {
                taskk.Key.GetAttackPointCollider().enabled = true;
            }
            else 
            {
                taskk.Key.GetTrailer().DetachTrailer();
                taskk.Key.GetAttackPointCollider().enabled = false;
                taskk.Key.CurrentStage = TrailerTask.TaskStage.TakeTrailer;

            }
            taskk.Key.CheckStage();
        }
    }
    private void Update()
    {
        if (_taskList.GetCurrentTask().Key != null && _taskList.GetCurrentTask().Key.TryToFinishTask())
        {
            _taskList.GetCurrentTask().Key.FinishTask();
            _taskList.GetCurrentTask().Key.GetTrailer().DetachTrailer();
            _taskList.GetCurrentTask().Key.GetAttackPointCollider().enabled = false;

            OnTaskCompleted?.Invoke();
        }
    }
    private void ChangeTaskStage(TrailerTask.TaskStage stageToSwitch)
    {
        if (_taskList.GetCurrentTask().Key.CurrentStage != TrailerTask.TaskStage.IsDone)
            _taskList.GetCurrentTask().Key.CurrentStage = stageToSwitch;
        _taskList.GetCurrentTask().Key.CheckStage();

    }
    private void OnDestroy()
    {
        foreach (var task in _taskList.GetTasks())
        {
            task.Key.GetTrailer().OnTrailerAttached -= ChangeTaskStage;
            task.Key.GetTrailer().OnTrailerDetached -= ChangeTaskStage;
        }
    }
    


}
