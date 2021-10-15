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

    private int _scoreAmount;

    public void FillUI(int score) //fill ui by using current Task price
    {
        _text.text = score.ToString();
    }
    public void MakeFinalScreenActive()
    {
        _finalScreen.SetActive(true);
    }
    public void MakeFinalScreenInActive()
    {
        _finalScreen.SetActive(false);
    }
    public void SetCurrentTaskPrice(TrailerTask task) 
    {
        _scoreAmount = task.GetTaskPrice();
    }
    public void SetScore(Button button)
    {
        var score = _player.GetData().Score += _scoreAmount;

        var newData = new PlayerData()
        {
            Score = score
        };

        if (_player.TryToSetData(newData))
        {
            button.interactable = false;  
        }
    }
    private void Start()
    {
        _finalScreen.SetActive(false);
    }
}
