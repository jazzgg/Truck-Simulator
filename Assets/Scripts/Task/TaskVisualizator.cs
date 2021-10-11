using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TaskVisualizator : MonoBehaviour
{
    [SerializeField]
    private TaskList _taskList;
    [SerializeField]
    private TaskActivator _taskActivator;

    private List<Image> _images;

    private void Start()
    {
        _taskActivator.OnTaskCompleted += RemoveImgTask;

        _images = new List<Image>(_taskList.GetTasks().Count);

        for (int i = 0; i < _taskList.GetTasks().Count; i++)
        {
            _images.Add(_taskList.GetTasks()[i].gameObject.GetComponent<Image>());
        }
    }
    private void RemoveImgTask()
    {
        _images.Remove(_taskList.GetCurrentTask().GetComponent<Image>());
    }

    public void VisualizateCurrentTask(TrailerTask task)
    {
        for (int i = 0; i < _taskList.GetTasks().Count; i++)
        {
            if (_taskList.GetTasks()[i] == task) _images[i].color = new Color(_images[i].color.r, _images[i].color.g, _images[i].color.b, 255);

            else
            {
                _images[i].color = new Color(_images[i].color.r, _images[i].color.g, _images[i].color.b, 0.2f);
            }
        }
    }
    private void OnDestroy()
    {
        _taskActivator.OnTaskCompleted -= RemoveImgTask;
    }
}
