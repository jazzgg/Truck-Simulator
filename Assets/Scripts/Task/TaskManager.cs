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
    private RCC_TruckTrailer _trailer;
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
    private TaskFinish _taskFinish;
    [SerializeField]
    private TaskPoints _points;

    private TrailerTask _task;

    public void StartTask(TrailerTask task) // called by button and takes task logic connected with task button which was clicked
    {
        _navigatorArrow.gameObject.SetActive(true);

        task.OnTaskCompleted += FinishTask;
        task.OnStateChanged += _gps.SetNewTarget;
        task.OnStateChanged += _navigatorArrow.SetTarget;
        task.OnTruckNearToTrailer += _camSwitcher.ActivateCustomCam;
        task.OnTruckFarFromTrailer += _camSwitcher.ReturnToDefaultCam;
        task.OnTryToFinishTask += _taskFinish.MakeButtonActive;
        task.OnNoneTryToFinishTask += _taskFinish.MakeButtonInActive;
         
        task.Initialize();

        _player.transform.position = task.GetStartPoint();
        _taskTriggersActivator.StartCoroutine(_taskTriggersActivator.DisableTriggersWithDelay(1));
        _taskFinish.SetCurrentTask(task);
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
        _task.OnTryToFinishTask -= _taskFinish.MakeButtonActive;
        _task.OnNoneTryToFinishTask -= _taskFinish.MakeButtonInActive;

        _taskList.GenerateNewTaskInsteadCurrent();
        _navigatorArrow.SetTarget(Vector3.zero);
        _gps.StopTargetFollow();

    }

    private void Start()
    {
        _taskList.InitializeFieldsForGenerate(_player.transform, _trailer, _points);

        _taskList.GenerateStartTasks();
    }

}
