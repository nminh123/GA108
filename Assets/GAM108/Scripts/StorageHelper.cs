using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageHelper
{
    private readonly string filename = "gamedata.txt";
    public static GameDataPlayed played;

    public void LoadData()
    {
        played = new GameDataPlayed()
        {
            plays = new List<GameData>()
        };

        string dataAsJson = StorageManager.LoadFromFile(filename);
        if (dataAsJson != null)
        {
            played = JsonUtility.FromJson<GameDataPlayed>(dataAsJson);
        }
    }

    public void SaveData()
    {
        string dataAsJson = JsonUtility.ToJson(played);
        StorageManager.SaveToFile(filename, dataAsJson);
    }
}
