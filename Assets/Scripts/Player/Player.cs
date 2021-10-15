using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private PlayerData _data;

    [SerializeField]
    private Text _score;

    public bool TryToSetData(PlayerData newData)
    {
        _data = newData;

        ChangeUI();

        return true;
    }
    public PlayerData GetData()
    {
        return _data;
    }
    private void Start()
    {
        ChangeUI();
    }
    private void ChangeUI()
    {
        _score.text = _data.Score.ToString();
    }
}

