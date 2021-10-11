using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskActivator : MonoBehaviour
{
    private TaskList _taskList => GetComponent<TaskList>();

    private void Awake()
    {
        _taskList.OnSetTask += DisableOtherTasks;

        for (int i = 0; i < _taskList.GetTasks().Length; i++)
        {
            _taskList.GetTasks()[i].StartTask();
        }
    }
    public void ActivateCurrentTask()
    {
        if (_taskList.GetCurrentTask().isDone == false)
            _taskList.GetCurrentTask().StartTask();
    }

    private void Update()
    {
        if (_taskList.GetCurrentTask() != null && _taskList.GetCurrentTask().TryToFinishTask())
        {
            _taskList.GetCurrentTask().GetTrailer().DetachTrailer();
            _taskList.GetCurrentTask().FinishTask();
        }
    }
    private void DisableOtherTasks()
    {
        for (int i = 0; i < _taskList.GetTasks().Length; i++)
        {
            if (_taskList.GetCurrentTask() != _taskList.GetTasks()[i])
            {
                _taskList.GetTasks()[i].enabled = false;
                if (_taskList.GetPreviousTask() != null)
                    _taskList.GetPreviousTask().GetTrailer().DetachTrailer();
                _taskList.GetTasks()[i].GetAttachPoint().enabled = false;
                if (_taskList.GetTasks()[i].isDone == false)
                    _taskList.GetTasks()[i].ActivateFirstStage();
            }
            else
            {
                _taskList.GetTasks()[i].enabled = true;
                _taskList.GetTasks()[i].GetAttachPoint().enabled = true;
            }
        }
    }
    private void OnDestroy()
    {
        _taskList.OnSetTask -= DisableOtherTasks;
    }
}
