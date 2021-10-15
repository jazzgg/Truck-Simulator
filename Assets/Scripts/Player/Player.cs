using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public delegate void OnValueChangedDel();

    public OnValueChangedDel OnScoreValueChanged;

    public PlayerData Data;

    [SerializeField]
    private Text _score;

    private void Start()
    {
        OnScoreValueChanged += ChangeUI;

        ChangeUI();
    }
    private void ChangeUI()
    {
        _score.text = Data.Score.ToString();
    }
    
    private void OnDestroy()
    {
        OnScoreValueChanged -= ChangeUI;
    }
}

