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

    public void VisualizateCurrentTask(KeyValuePair<TrailerTask, TaskVisusalization> task)
    {
        foreach (var task1 in _taskList.GetTasks())
        {
            if (task1.Key == task.Key)
            {
                task1.Value.MakeActive();
            }

            else task1.Value.MakeInactive();
        }
    }
}
