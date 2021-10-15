using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TrailerTask : MonoBehaviour
{
    public event Action<Vector3> OnStateChanged; //this one created for gps and navigator arrow
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
    public void Initialize() //called when task become active
    {
        _trailer.gameObject.SetActive(true);

        CurrentStage = TaskStage.TakeTrailer;
        SetStage();
    }
    public bool TryToFinishTask() //Check distance btw trailer and finish point
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
    public void SetStage() //Set Stage and choose necessary settings
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
    #endregion 
    private void OnTrailerAttachedChangeStage() // called when trailer is attached
    {
        CurrentStage = TaskStage.TakeOutTrailer;
        SetStage();
    }
    private void Start()
    {
        _trailer.OnTrailerAttached += OnTrailerAttachedChangeStage;
        _trailer.gameObject.SetActive(false);

        _attachPointCollider = _trailer.GetComponentInChildren<RCC_TrailerAttachPoint>().GetComponent<BoxCollider>();
    }
    private void Update()
    {
        if (isWorking && TryToFinishTask())
        {
            CurrentStage = TaskStage.IsDone;
            SetStage(); //use for update state
        }
    }
    private void OnDestroy()
    {
        _trailer.OnTrailerAttached -= OnTrailerAttachedChangeStage;
    }
}
