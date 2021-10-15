using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TrailerTask : MonoBehaviour
{
    public event Action<Vector3> OnStateChanged;
    public event Action OnTaskCompleted;
    public bool isDone { get; private set; }
    [HideInInspector]
    public bool isWorking = false;

    [SerializeField]
    private RCC_TruckTrailer _trailer;
    [SerializeField]
    private Transform _finishPoint;
    [SerializeField]
    private Transform _startPoint;
    [SerializeField]
    private float _minDistance;
    [SerializeField]
    private int _taskPrice;

    private BoxCollider _attachPointCollider;
    private Vector3 _currentTarget;
    private TaskStage CurrentStage;

    public enum TaskStage
    {
        TakeTrailer,
        TakeOutTrailer,
        IsDone
    }
    public void Initialize()
    {
        CurrentStage = TaskStage.TakeTrailer;
        SetStage();
    }
    public bool TryToFinishTask()
    {
        if (Vector3.Distance(_trailer.transform.position, _finishPoint.position) < _minDistance)
        {
            isWorking = false;
            CurrentStage = TaskStage.IsDone;
            SetStage();

            return true;
        }

        return false;
    }
    public void SetStage()
    {
        switch (CurrentStage)
        {
            case TaskStage.TakeTrailer:
                _currentTarget = _trailer.transform.position;
                OnStateChanged?.Invoke(_currentTarget);
                break;
            case TaskStage.TakeOutTrailer:
                _currentTarget = _finishPoint.position;
                OnStateChanged?.Invoke(_currentTarget);
                break;
            case TaskStage.IsDone:
                isDone = true;
                OnTaskCompleted?.Invoke();
                break;
        }
    }
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
        return _finishPoint.position;
    }
    public Vector3 GetCurrentTarget()
    {
        return _currentTarget;
    }
    public Vector3 GetStartPoint()
    {
        return _startPoint.position;
    }
    public int GetTaskPrice()
    {
        return _taskPrice;
    }
    private void OnTrailerAttachedChangeStage()
    {
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
        if (isWorking && TryToFinishTask())
        {
            CurrentStage = TaskStage.IsDone;
            SetStage();
        }
    }
    private void OnDestroy()
    {
        _trailer.OnTrailerAttached -= OnTrailerAttachedChangeStage;
    }
}
