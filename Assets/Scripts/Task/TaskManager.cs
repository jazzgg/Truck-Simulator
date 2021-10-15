using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TaskManager : MonoBehaviour
{
    //this class enable everything what connected with task

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
    [SerializeField]
    private CamerasSwitcher _camSwitcher;

    private TrailerTask _task;

    public void StartTask(TrailerTask task) // called by button and takes task logic connected with task button which was clicked
    {
        _navigatorArrow.gameObject.SetActive(true);

        task.isWorking = true;

        task.OnTaskCompleted += FinishTask;
        task.OnStateChanged += _gps.SetNewTarget;
        task.OnStateChanged += _navigatorArrow.SetTarget;
        task.OnTruckNearToTrailer += _camSwitcher.ActivateCustomCam;
        task.OnTruckFarFromTrailer += _camSwitcher.ReturnToDefaultCam;
         
        task.Initialize();

        _player.transform.position = task.GetStartPoint();
        _taskTriggersActivator.StartCoroutine(_taskTriggersActivator.DisableTriggersWithDelay(1));
        _task = task;
    }
    public void FinishTask() // called by current task event (OnTaskCompleted) 
    {
        _navigatorArrow.gameObject.SetActive(false);

        _task.OnStateChanged -= _gps.SetNewTarget;
        _task.OnStateChanged -= _navigatorArrow.SetTarget;
        _task.OnTaskCompleted -= FinishTask;
        _task.OnTruckNearToTrailer -= _camSwitcher.ActivateCustomCam;
        _task.OnTruckFarFromTrailer -= _camSwitcher.ReturnToDefaultCam;

        var taskPrice = _task.GetTaskPrice();

        _task.GetTrailer().DetachTrailer();
        _taskFinalScreen.FillUI(taskPrice);
        _taskFinalScreen.MakeFinalScreenActive();
        _taskFinalScreen.SetCurrentTaskPrice(taskPrice);
        _taskList.RemoveCurrentTask(_task);
        _taskTriggersActivator.EnableTriggers();
        _navigatorArrow.SetTarget(Vector3.zero);
        _gps.StopTargetFollow();

    }

}
