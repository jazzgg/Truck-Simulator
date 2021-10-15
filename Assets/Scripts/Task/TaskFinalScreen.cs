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

    private int _scoreAmount;

    public void FillUI(int score) //fill UI by using current Task price
    {
        _text.text = score.ToString();
    }
    public void MakeFinalScreenActive()
    {
        _finalScreen.SetActive(true);
        _getButton.interactable = true;
    }
    public void MakeFinalScreenInActive()
    {
        _finalScreen.SetActive(false);
    }
    public void SetCurrentTaskPrice(int score) 
    {
        _scoreAmount = score;
    }
    public void SetScore()
    {
        _player._data.Score += _scoreAmount; 

        _getButton.interactable = false;
    }
    private void Start()
    {
        _finalScreen.SetActive(false);
    }
}
