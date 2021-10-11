using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TaskVisualizator : MonoBehaviour
{
    private TaskList _taskList;
    private Image[] _tasksImages;

    private Image _currentTaskImg;

    private void Start()
    {
        _taskList = GetComponent<TaskList>(); 
        _currentTaskImg = _taskList.GetCurrentTask().GetComponent<Image>();

        _tasksImages = new Image[_taskList.GetTasks().Length];

        for (int i = 0; i < _tasksImages.Length; i++)
        {
            _tasksImages[i] = _taskList.GetTasks()[i].GetComponent<Image>();
        }
    }

    public void VisualizeSelectedTask()
    {
        for (int i = 0; i < _tasksImages.Length; i++)
        {
            if (_taskList.GetCurrentTask() == _taskList.GetTasks()[i])
                _currentTaskImg.color = new Color(_tasksImages[i].color.r, _tasksImages[i].color.g, _tasksImages[i].color.b, 255f);
            else
                _tasksImages[i].color = new Color(_tasksImages[i].color.r, _tasksImages[i].color.g, _tasksImages[i].color.b, 0.2f);
        }
    }
}
