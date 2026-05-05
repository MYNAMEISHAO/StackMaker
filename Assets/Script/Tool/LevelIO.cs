using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class LevelIO
{
    private static string SavePath => Path.Combine(Application.dataPath, "level_data.json");

    public static LevelDataBase LoadDatabase()
    {
        if (!File.Exists(SavePath)) return new LevelDataBase { levels = new List<LevelData>() };
        string json = File.ReadAllText(SavePath);
        return JsonUtility.FromJson<LevelDataBase>(json);
    }

    public static void SaveDatabase(LevelDataBase db)
    {
        string json = JsonUtility.ToJson(db, true);

        File.WriteAllText(SavePath, json);
    }
}