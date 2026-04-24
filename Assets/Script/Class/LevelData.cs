using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public string levelName;
    public int width;
    public int length;
    public Vector3Int origin;
    public List<Block> gridData;
    public Vector3Int startPos;

    public LevelData(string levelName, int width, int length, Vector3Int origin, List<Block> gridData, Vector3Int startPos)
    {
        this.levelName = levelName;
        this.width = width;
        this.length = length;
        this.origin = origin;
        this.gridData = gridData;
        this.startPos = startPos;
    }
}

[Serializable]
public class LevelDataBase
{
    public List<LevelData> levels;

    public LevelDataBase()
    {
        this.levels = new List<LevelData>();
    }
}