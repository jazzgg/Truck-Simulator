using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TaskFinish : MonoBehaviour
{
    public bool isTrailerDetached;

    [SerializeField]
    private Button _detachButton;
    [SerializeField]
    private TaskFinalScreen _taskFinalScreen;

    private RCC_TruckTrailer _currentTrailer;
    private TrailerTask _currentTask;
    
    public void SetCurrentTask(TrailerTask task)
    {
        isTrailerDetached = false;

        _currentTrailer = task.GetTrailer();
        _currentTask = task;
    }
    public void Detach()
    {
        _currentTask.FinishTask();

        _currentTrailer.DetachTrailer();

        _taskFinalScreen.MakeFinalScreenActive();

        MakeButtonInActive();
    }
    public void MakeButtonActive()
    {
        if (_detachButton.gameObject.activeInHierarchy == false && isTrailerDetached == false)
            _detachButton.gameObject.SetActive(true);
    }
    public void MakeButtonInActive()
    {
        if (_detachButton.gameObject.activeInHierarchy)
            _detachButton.gameObject.SetActive(false);
    }
    private void Start()
    {
        _detachButton.gameObject.SetActive(false);
    }

}
