using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class TaskList : MonoBehaviour 
{
    [SerializeField]
    private List<TrailerTask> _tasks;
    [SerializeField]
    private List<Button> _tasksVis;

    public void RemoveCurrentTask(TrailerTask trailerTask)
    {
        int index = _tasks.IndexOf(trailerTask);

        VisualizeRemovedTask(index);

        _tasksVis.RemoveAt(index);
        _tasks.RemoveAt(index);
    }
    private void VisualizeRemovedTask(int index)
    {
        _tasksVis[index].interactable = false;
    }
}
 