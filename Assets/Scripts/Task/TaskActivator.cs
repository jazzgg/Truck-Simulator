using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TaskActivator : MonoBehaviour
{
    public event Action OnTaskCompleted;

    [SerializeField]
    private GPS _gps;

    private TaskList _taskList;

    public void DisableOtherTask(KeyValuePair<TrailerTask, TaskVisusalization> task)
    {
        foreach (var taskInList in _taskList.GetTasks())
        {
            if (taskInList.Key == task.Key)
            {
                taskInList.Key.GetAttackPointCollider().enabled = true;
            }
            else 
            {
                taskInList.Key.GetTrailer().DetachTrailer();
                taskInList.Key.GetAttackPointCollider().enabled = false;
                taskInList.Key.CurrentStage = TrailerTask.TaskStage.TakeTrailer;

            }
            taskInList.Key.CheckStage();
        }
    }
    private void ChangeTaskStage(TrailerTask.TaskStage stageToSwitch)
    {
        if (_taskList.GetCurrentTask().Key.CurrentStage != TrailerTask.TaskStage.IsDone)
            _taskList.GetCurrentTask().Key.CurrentStage = stageToSwitch;
        if (stageToSwitch == TrailerTask.TaskStage.TakeOutTrailer) _gps.SetNewTarget(_taskList.GetCurrentTask().Key.GetFinishPoint());
            _taskList.GetCurrentTask().Key.CheckStage();

    }
    private void Start()
    {
        _taskList = GetComponent<TaskList>();

        foreach (var task in _taskList.GetTasks())
        {
            task.Key.GetTrailer().OnTrailerAttached += ChangeTaskStage;
            task.Key.GetTrailer().OnTrailerDetached += ChangeTaskStage;
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
    private void OnDestroy()
    {
        foreach (var task in _taskList.GetTasks())
        {
            task.Key.GetTrailer().OnTrailerAttached -= ChangeTaskStage;
            task.Key.GetTrailer().OnTrailerDetached -= ChangeTaskStage;
        }
    }
    


}
