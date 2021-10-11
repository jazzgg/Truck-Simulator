using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TaskActivator : MonoBehaviour
{
    public event Action OnTaskCompleted;

    [SerializeField]
    private TaskList _taskList;

    private void Start()
    {
        for (int i = 0; i < _taskList.GetTasks().Count; i++)
        {
            _taskList.GetTasks()[i].GetTrailer().OnTrailerAttached += ChangeTaskStage;
            _taskList.GetTasks()[i].GetTrailer().OnTrailerDetached += ChangeTaskStage;
        }

    }
    public void DisableOtherTask(TrailerTask task)
    {
        for (int i = 0; i < _taskList.GetTasks().Count; i++)
        {
            if (_taskList.GetTasks()[i] == task)
            {
                _taskList.GetTasks()[i].GetAttackPointCollider().enabled = true;
            }
            else 
            {
                _taskList.GetTasks()[i].GetTrailer().DetachTrailer();
                _taskList.GetTasks()[i].GetAttackPointCollider().enabled = false;
                _taskList.GetTasks()[i].CurrentStage = TrailerTask.TaskStage.TakeTrailer;

            }
            _taskList.GetTasks()[i].CheckStage();
        }
    }
    private void Update()
    {
        if (_taskList.GetCurrentTask() != null && _taskList.GetCurrentTask().TryToFinishTask())
        {
            _taskList.GetCurrentTask().FinishTask();
            _taskList.GetCurrentTask().GetTrailer().DetachTrailer();
            _taskList.GetCurrentTask().GetAttackPointCollider().enabled = false;

            OnTaskCompleted?.Invoke();
        }
    }
    private void ChangeTaskStage(TrailerTask.TaskStage stageToSwitch)
    {
        if (_taskList.GetCurrentTask().CurrentStage != TrailerTask.TaskStage.IsDone)
            _taskList.GetCurrentTask().CurrentStage = stageToSwitch;
        _taskList.GetCurrentTask().CheckStage();

    }
    private void OnDestroy()
    {
        for (int i = 0; i < _taskList.GetTasks().Count; i++)
        {
            _taskList.GetTasks()[i].GetTrailer().OnTrailerAttached -= ChangeTaskStage;
            _taskList.GetTasks()[i].GetTrailer().OnTrailerDetached -= ChangeTaskStage;
        }
    }
    


}
