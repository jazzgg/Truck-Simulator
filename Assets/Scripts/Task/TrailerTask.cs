using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TrailerTask : MonoBehaviour
{
    public TaskStage CurrentStage;
    public bool isDone { get; private set; }

    [SerializeField]
    private RCC_TruckTrailer _trailer;
    [SerializeField]
    private Transform _finishPoint;
    [SerializeField]
    private float _minDistance;
    [SerializeField]
    private string _takeTrailerText, _takeOutTrailerText, _isDoneText;
    [SerializeField]
    private Text _stageText;
    [SerializeField]
    private Button _taskButton;
    [SerializeField]
    private Toggle _isDoneToggle;

    private BoxCollider _attachPointCollider;

 
    public enum TaskStage
    {
        TakeTrailer,
        TakeOutTrailer,
        IsDone
    }

    public bool TryToFinishTask()
    {
        if (Vector3.Distance(_trailer.transform.position, _finishPoint.position) < _minDistance)
        {
            CurrentStage = TaskStage.IsDone;
            CheckStage();

            return true;
        }

        return false;
    }
    public void CheckStage()
    {
        switch (CurrentStage)
        {
            case TaskStage.TakeTrailer:
                _stageText.text = _takeTrailerText;
                break;
            case TaskStage.TakeOutTrailer:
                _stageText.text = _takeOutTrailerText;
                break;
            case TaskStage.IsDone:
                _stageText.text = _isDoneText;
                isDone = true;
                _isDoneToggle.isOn = isDone;
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
    public void FinishTask()
    {
        _taskButton.interactable = false;
    }
    private void Start()
    {
        CurrentStage = TaskStage.TakeTrailer;
        _isDoneToggle.isOn = false;
        _attachPointCollider = _trailer.GetComponentInChildren<RCC_TrailerAttachPoint>().GetComponent<BoxCollider>();

        CheckStage();
    }
}
