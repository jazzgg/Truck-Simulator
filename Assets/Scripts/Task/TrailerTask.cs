using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TrailerTask : MonoBehaviour
{
    //events
    #region
    public event Action<Vector3> OnStateChanged; //this one created for gps and navigator arrow
    public event Action OnTaskCompleted;
    public event Action OnTruckNearToTrailer;
    public event Action OnTruckFarFromTrailer;
    public event Action OnTryToFinishTask;
    public event Action OnNoneTryToFinishTask;
    #endregion
    public bool isDone { get; private set; }
    [HideInInspector]
    public bool isWorking = false;
    #region

    private Transform _player;

    private RCC_TruckTrailer _trailer;

    private Vector3 _finishPoint;

    private Vector3 _startPoint;

    private float _minDistance = 15f; // min distance to finish for win

    private float _minDistanceToTrailer = 20f;

    private int _taskPrice;
    #endregion

    private BoxCollider _attachPointCollider;
    private Vector3 _currentTarget;
    private TaskStage CurrentStage;

    public string _taskText;

    public TrailerTask Constructor(Transform player, RCC_TruckTrailer trailer, TaskPoints points)
    {
        _player = player;
        _trailer = trailer;

        _startPoint = points.GetRandomPoint().position;
        _finishPoint = points.GetRandomPoint().position;

        CheckPointsEqual(_startPoint, _finishPoint, points);

        _trailer.transform.position = points.GetRandomPoint().position;

        CheckPointsEqual(_trailer.transform.position, _startPoint, points);
        CheckPointsEqual(_trailer.transform.position, _startPoint, points);

        var firstPartOfTaskPrice = Mathf.FloorToInt(points.GetDistance(_startPoint, _trailer.transform.position));
        var secondPartOfTaskPrice = Mathf.FloorToInt(points.GetDistance(_trailer.transform.position, _finishPoint));

        _taskPrice = firstPartOfTaskPrice + secondPartOfTaskPrice;

        _taskText = $"Заберите прицеп довезите его до точки {UnityEngine.Random.Range(0, 10)}  + {_taskPrice}";

        return this;

    }
    public enum TaskStage
    {
        TakeTrailer,
        TakeOutTrailer,
        IsDone
    }
    public void Initialize() //called when task become active
    {
        _trailer.gameObject.SetActive(true);

        CurrentStage = TaskStage.TakeTrailer;
        SetStage();

        isWorking = true;
    }
    public void TryToFinishTask() //Check distance btw trailer and finish point
    {
        if (Vector3.Distance(_trailer.transform.position, _finishPoint) < _minDistance)
        {
            OnTryToFinishTask?.Invoke();
        }
        else OnNoneTryToFinishTask?.Invoke();
    }
    public void SetStage() //Set Stage and choose necessary settings
    {
        switch (CurrentStage)
        {
            case TaskStage.TakeTrailer:
                _currentTarget = _trailer.transform.position;
                OnStateChanged?.Invoke(_currentTarget);
                break;
            case TaskStage.TakeOutTrailer:
                _currentTarget = _finishPoint;
                OnStateChanged?.Invoke(_currentTarget);
                break;
            case TaskStage.IsDone:
                isWorking = false;
                isDone = true;
                OnTaskCompleted?.Invoke();
                break;
        }
    }
    public void FinishTask()
    {
        CurrentStage = TaskStage.IsDone;

        SetStage();
    }
    //Get Functions Region
    #region 
    public BoxCollider GetAttackPointCollider()
    {
        return _attachPointCollider;
    }
    public RCC_TruckTrailer GetTrailer()
    {
        return _trailer;
    }
    public Vector3 GetFinishPoint()
    {
        return _finishPoint;
    }
    public Vector3 GetCurrentTarget()
    {
        return _currentTarget;
    }
    public Vector3 GetStartPoint()
    {
        return _startPoint;
    }
    public int GetTaskPrice()
    {
        return _taskPrice;
    }
    #endregion 
    private void OnTrailerAttachedChangeStage() // called when trailer is attached
    {
        OnTruckFarFromTrailer?.Invoke(); // this one for switch cam to default mode

        CurrentStage = TaskStage.TakeOutTrailer;
        SetStage();
    }
    private void Start()
    {
        _trailer.OnTrailerAttached += OnTrailerAttachedChangeStage;

        _attachPointCollider = _trailer.GetComponentInChildren<RCC_TrailerAttachPoint>().GetComponent<BoxCollider>();
    }
    private void Update()
    {
        if (isWorking)
        {
            TryToFinishTask();
        }
        if (CurrentStage == TaskStage.TakeTrailer
            && Vector3.Distance(_trailer.transform.position, _player.position) < _minDistanceToTrailer)
        {
            OnTruckNearToTrailer?.Invoke();
        }
        else if(CurrentStage == TaskStage.TakeTrailer) OnTruckFarFromTrailer?.Invoke();
    }
    private void CheckPointsEqual(Vector3 point1, Vector3 point2, TaskPoints points)
    {
        if (point1 == point2)
        {
            point2 = points.GetRandomPoint().position;
            CheckPointsEqual(point1, point2, points);
        }
    }
    private void OnDestroy()
    {
        _trailer.OnTrailerAttached -= OnTrailerAttachedChangeStage;
    }
}
