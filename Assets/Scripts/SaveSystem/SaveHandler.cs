using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SaveHandler : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    private SaveSystem _saveSystem;

    private void Awake()
    {
        _saveSystem = new SaveSystem();

        Load();
    }
    public void Load()
    {
        _player.SetData(_saveSystem.Load());
    }
    public void Save()
    {
       _saveSystem.Save(_player.GetData());
    }
    private void OnApplicationQuit()
    {
        Save();
    }

}
