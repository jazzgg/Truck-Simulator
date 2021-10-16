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
    [SerializeField]
    private TaskFinish _trailerDetacher;

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
        task.OnTryToFinishTask += _trailerDetacher.MakeButtonActive;
        task.OnNoneTryToFinishTask += _trailerDetacher.MakeButtonInActive;
         
        task.Initialize();

        _player.transform.position = task.GetStartPoint();
        _taskTriggersActivator.StartCoroutine(_taskTriggersActivator.DisableTriggersWithDelay(1));
        _trailerDetacher.SetCurrentTask(task);
        _taskFinalScreen.SetCurrentTaskPrice(task.GetTaskPrice());
        _taskFinalScreen.FillUI(task.GetTaskPrice());

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
        _task.OnTryToFinishTask -= _trailerDetacher.MakeButtonActive;
        _task.OnNoneTryToFinishTask -= _trailerDetacher.MakeButtonInActive;

        _taskList.RemoveCurrentTask(_task);
        _navigatorArrow.SetTarget(Vector3.zero);
        _gps.StopTargetFollow();

    }

}
