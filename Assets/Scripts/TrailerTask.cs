using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TrailerTask : MonoBehaviour
{
    public bool isDone { get; protected set; }

    [SerializeField]
    private string _firstStagetText, _secondStageText, _taskCompletedText;
    [SerializeField]
    private float _minDistance;
    [SerializeField]
    private RCC_TruckTrailer _trailer;
    [SerializeField]
    private Transform _finishPoint;
    private Text _taskText => GetComponentInChildren<Text>();
    private Toggle _isTaskDoneToggle => GetComponentInChildren<Toggle>();
    private BoxCollider _attachPointCollider => _trailer.GetComponentInChildren<RCC_TrailerAttachPoint>().GetComponent<BoxCollider>();

    private Button _taskButton => GetComponent<Button>();
    public RCC_TruckTrailer GetTrailer()
    {
        return _trailer;
    }
    public BoxCollider GetAttachPoint()
    {
        return _attachPointCollider;
    }
    public bool TryToFinishTask()
    {
        if (Vector3.Distance(_trailer.transform.position, _finishPoint.position) < _minDistance)
        {
            return true;
        }

        return false;
    }
    public void StartTask()
    {
        _trailer.OnTrailerAttached += ActivateSecondStage;

        _isTaskDoneToggle.isOn = isDone;
        _taskText.text = _firstStagetText;
    }
    public void ActivateFirstStage()
    {
        _taskText.text = _firstStagetText;
    }
    private void ActivateSecondStage()
    {
        _taskText.text = _secondStageText;
    }
    public void FinishTask()
    {
        _trailer.OnTrailerAttached -= ActivateSecondStage;

        isDone = true;

        _isTaskDoneToggle.isOn = isDone;
        _taskText.text = _taskCompletedText;
        _taskButton.interactable = false;
    }

}
