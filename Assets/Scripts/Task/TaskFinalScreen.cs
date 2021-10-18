using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskFinalScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject _finalScreen;
    [SerializeField]
    private Text _text;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private Button _getButton;
    [SerializeField]
    private SaveHandler _saveHandler;
    [SerializeField]
    private TaskFinish _finish;
    [SerializeField]
    private TaskTriggersActivator _taskTriggersActivator;

    private int _scoreAmount;

    public void FillUI(int score) //fill UI by using current Task price
    {
        _text.text = score.ToString();
    }
    public void MakeFinalScreenActive()
    {
        _finish.isTrailerDetached = true;

        _finalScreen.SetActive(true);
        _getButton.interactable = true;

        _finish.MakeButtonInActive();
    }
    public void MakeFinalScreenInActive()
    {
        _finalScreen.SetActive(false);
        _taskTriggersActivator.EnableTriggers();
    }
    public void SetCurrentTaskPrice(int score) 
    {
        _scoreAmount = score;
    }
    public void SetScore()
    {
        _player._data.Score += _scoreAmount; 

        _getButton.interactable = false;

        _saveHandler.Save();
    }
    private void Start()
    {
        _finalScreen.SetActive(false);
    }
}
