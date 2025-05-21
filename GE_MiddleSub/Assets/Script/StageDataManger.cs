using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]

public class StageResult
{
    public string playerName;
    public int stage;
    public int score;
}
[System.Serializable]
public class StageResultList
{
    public List<StageResult> results = new List<StageResult>();
}
public static class StageResulSaver
{
    private const string FILE = "stage_results.json";
    private const string PLAYER_NAME = "PlayerName";
    private static string filepath = Path.Combine(Application.persistentDataPath, FILE);
    public static void SaveStage(int stage, int score)
    {
        StageResultList list = LoadInternal();
        string playername = PlayerPrefs.GetString(PLAYER_NAME, "");
        StageResult entry = new StageResult
        {
            playerName = playername,
            stage = stage,
            score = score
        };
        list.results.Add(entry);
        string Json = JsonUtility.ToJson(list, true);
        File.WriteAllText(filepath, Json);
    }
    public static StageResultList LoadRank()
    {
        return LoadInternal();
    }
    private static StageResultList LoadInternal()
    {
        if (!File.Exists(filepath))
            return new StageResultList();
        string Json = File.ReadAllText(filepath);
        StageResultList list = JsonUtility.FromJson<StageResultList>(Json);
        if (list == null)
            return new StageResultList();
        else
            return list;
    }
}

