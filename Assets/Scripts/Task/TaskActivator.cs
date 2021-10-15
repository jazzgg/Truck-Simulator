using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TaskActivator : MonoBehaviour
{
    public event Action OnTaskCompleted;

    [SerializeField]
    private GPS _gps;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private TaskFinalScreen _taskFinalScreen;
    [SerializeField]
    private TaskList _taskList;
    [SerializeField]
    private TaskTriggersActivator _taskTriggersActivator;
    [SerializeField]
    private NavigatorArrow _navigatorArrow;

    private TrailerTask _task;

    public void StartTask(TrailerTask task)
    {
        _navigatorArrow.gameObject.SetActive(true);

        task.isWorking = true;

        task.OnTaskCompleted += FinishTask;
        task.OnStateChanged += _gps.SetNewTarget;
        task.OnStateChanged += _navigatorArrow.SetTarget;
         
        task.Initialize();

        _player.transform.position = task.GetStartPoint();
        _taskTriggersActivator.StartCoroutine(_taskTriggersActivator.DisableTriggersWithDelay(1));
        _task = task;
    }
    public void FinishTask()
    {
        _navigatorArrow.gameObject.SetActive(false);

        _task.OnStateChanged -= _gps.SetNewTarget;
        _task.OnStateChanged -= _navigatorArrow.SetTarget;
        _task.OnTaskCompleted -= FinishTask;

        var taskPrice = _task.GetTaskPrice();

        _task.GetTrailer().DetachTrailer();
        _taskFinalScreen.FillUI(taskPrice);
        _taskFinalScreen.MakeFinalScreenActive();
        _taskFinalScreen.SetScore(taskPrice);
        _taskList.RemoveCurrentTask(_task);
        _taskTriggersActivator.EnableTriggers();
        _navigatorArrow.SetTarget(Vector3.zero);
        _gps.StopTargetFollow();

    }

}
