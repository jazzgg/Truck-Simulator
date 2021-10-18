using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class TaskList : MonoBehaviour 
{
    public event Action OnClickTaskButton;

    [SerializeField]
    private Text[] _prices;
    [SerializeField]
    private Button[] _buttons;
    [SerializeField]
    private TaskManager _taskManager;

    private List<TrailerTask> _tasks;

    private Transform _player;
    private RCC_TruckTrailer _trailer;
    private TaskPoints _points;

    private KeyValuePair<int, TrailerTask> _currentTask;



    public TrailerTask GenerateNewTask(int index)
    {
        var newTask = gameObject.AddComponent<TrailerTask>().Constructor(_player, _trailer, _points);
        
        _buttons[index].onClick.AddListener( () => _currentTask = new KeyValuePair<int, TrailerTask>(index, newTask));
        _buttons[index].onClick.AddListener( () => _taskManager.StartTask(_currentTask.Value));

        _prices[index].text = newTask._taskText;

        return newTask;
    }
    public void GenerateNewTaskInsteadCurrent()
    {
        var newTask = gameObject.AddComponent<TrailerTask>().Constructor(_player, _trailer, _points);

        var index = _currentTask.Key;

        _tasks.RemoveAt(index);
        _tasks.Add(newTask);

        _prices[index].text = newTask._taskText;

        _buttons[index].onClick.AddListener( () => _currentTask = new KeyValuePair<int, TrailerTask>(index, newTask));
        _buttons[index].onClick.AddListener( () => _taskManager.StartTask(_currentTask.Value));
    }
    public void GenerateStartTasks()
    {
        _tasks = new List<TrailerTask>(_buttons.Length);

        for (int i = 0; i < _buttons.Length; i++)
        {
            _tasks.Add(GenerateNewTask(i));
        }

    }
    public void InitializeFieldsForGenerate(Transform player, RCC_TruckTrailer trailer, TaskPoints points)
    {
        _player = player;
        _trailer = trailer;
        _points = points;
    }

}
 