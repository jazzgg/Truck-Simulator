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

    public void FillUI(int score)
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
    public void SetScore(int score)
    {
        _scoreAmount = score;
    }
    public void GetScore()
    {
        _player.Data.Score += _scoreAmount;
        _player.OnScoreValueChanged?.Invoke();
    }
    private void Start()
    {
        _finalScreen.SetActive(false);
    }
}
