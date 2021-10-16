using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem 
{
    private string _path;

    public SaveSystem()
    {
        _path = Application.persistentDataPath + "/SaveData.json";
    }
    public void Save(PlayerData data)
    {
        var json = JsonUtility.ToJson(data);

        using (var writer = new StreamWriter(_path))
        {
            writer.WriteLine(json);
        }
    }
    public PlayerData Load()
    {
        string json = "";

        using (var reader = new StreamReader(_path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                json += line;
            }
        }

        if (string.IsNullOrEmpty(json)) return new PlayerData();

        return JsonUtility.FromJson<PlayerData>(json);
    }
}
