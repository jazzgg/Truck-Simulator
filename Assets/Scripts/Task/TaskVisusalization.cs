using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskVisusalization : MonoBehaviour
{
    [SerializeField]
    private Image _taskImg;

    public void MakeInactive()
    {
        _taskImg.color = new Color(_taskImg.color.r, _taskImg.color.g, _taskImg.color.b, 0.2f);
    }
    public void MakeActive()
    {
        _taskImg.color = new Color(_taskImg.color.r, _taskImg.color.g, _taskImg.color.b, 1f);
    }
    public Image GetTaskImage()
    {
        return _taskImg;
    }
}
