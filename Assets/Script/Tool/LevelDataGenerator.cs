using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelDataGenerator : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap obstacleTilemap;
    [SerializeField] private Tilemap bridgeTilemap;
    [SerializeField] private Tilemap brickTilemap;
    [SerializeField] private Tilemap goalTilemap;
    [SerializeField] private Transform playerStart;
    [SerializeField] private string levelSaveName;
    private Block[,] tempGrid;
    int length, width;
    [SerializeField] private Vector3Int origin;
    [SerializeField] private Vector3Int playerGridPos;
    

    string saveFileName = "level_data.json";
    LevelDataBase levelDataBase;
    
    [ExecuteInEditMode]
    public void GenerateLevel()
    {
        BoundsInt b1 = GetBounds(obstacleTilemap);
        BoundsInt b2 = GetBounds(brickTilemap);
        BoundsInt b3 = GetBounds(bridgeTilemap);
        BoundsInt b4 = GetBounds(goalTilemap);
        
        int minX = Mathf.Min(b1.xMin,b2.xMin ,b3.xMin,b4.xMin);
        int minY = Mathf.Min(b1.yMin,b2.yMin ,b3.yMin, b4.yMin);
        int maxX = Mathf.Max(b1.xMax,b2.xMax ,b3.xMax, b4.xMax);
        int maxY = Mathf.Max(b1.yMax,b2.yMax ,b3.yMax, b4.yMax);

        width = maxX - minX;
        length = maxY - minY;

        origin = new Vector3Int(minX, minY, 0);
        playerGridPos = grid.WorldToCell(playerStart.position);
        tempGrid = new Block[width, length];

        AddDataToGrid(obstacleTilemap, -1, minX, minY);
        AddDataToGrid(bridgeTilemap, 2, minX, minY);
        AddDataToGrid(brickTilemap, 1, minX, minY);
        AddDataToGrid(goalTilemap, 3, minX, minY);
        
        Debug.Log("Grid Generated!");


    }
    private BoundsInt GetBounds(Tilemap tilemap)
    {
        if (tilemap.transform.childCount == 0) return new BoundsInt();

        Vector3Int min = new Vector3Int(int.MaxValue, int.MaxValue, 0);
        Vector3Int max = new Vector3Int(int.MinValue, int.MinValue, 0);

        foreach (Transform child in tilemap.transform)
        {
            Vector3Int cellPos = tilemap.WorldToCell(child.position);

            min.x = Mathf.Min(min.x, cellPos.x);
            min.y = Mathf.Min(min.y, cellPos.y);
            max.x = Mathf.Max(max.x, cellPos.x);
            max.y = Mathf.Max(max.y, cellPos.y);
        }
        return new BoundsInt(min.x, min.y, 0, max.x - min.x + 1, max.y - min.y + 1, 1);
    }
    private void AddDataToGrid(Tilemap map, int value, int minX, int minY)
    {
        foreach(Transform child in map.transform)
        {
            Vector3Int cellPos = map.WorldToCell(child.position);

            int gridPosX = cellPos.x - minX;
            int gridPosY = cellPos.y - minY;

            if(gridPosX >= 0 && gridPosY >= 0 && gridPosX < width && gridPosY < length)
            {
                if(Mathf.Abs(child.eulerAngles.z) <= 0.1f)
                {
                    tempGrid[gridPosX, gridPosY] = new Block(value,child.rotation);
                }
                else
                {
                    tempGrid[gridPosX, gridPosY] = new Block(-value,child.rotation);
                }
                Debug.Log("vị trí " + gridPosX + " " + gridPosY + " là " + value);
            }
        }
    }
   
    public void SaveToJson()
    {
        if(origin == null || playerStart == null || levelSaveName == null)
        {
            Debug.LogError("Hãy nhập đầy đủ thông tin");
            return;
        }
        Vector3Int playerOffset = playerGridPos - origin;
        // Tạo object Level mới
        LevelData newLevel = new LevelData(
            levelSaveName,
            width,
            length,
            Vector3Int.zero,
            new List<Block>(),
            playerOffset
            );

        for(int y = 0;y < length; y++)
        {
            for(int x = 0; x < width ; x++)
            {
                newLevel.gridData.Add(tempGrid[x, y]);
            }
        }

        string saveFilePath = Path.Combine(Application.persistentDataPath, saveFileName);
        levelDataBase = new LevelDataBase();
        if (File.Exists(saveFilePath))
        {
            string jsonRead = File.ReadAllText(saveFilePath);
            levelDataBase = JsonUtility.FromJson<LevelDataBase>(jsonRead); 
        }
        if (levelDataBase.levels == null) levelDataBase.levels = new List<LevelData>();

        int index = levelDataBase.levels.FindIndex(l => l.levelName == levelSaveName);
        if (index != -1) levelDataBase.levels[index] = newLevel;
        else levelDataBase.levels.Add(newLevel);

        string jsonSave = JsonUtility.ToJson(levelDataBase, true);
        Debug.Log(jsonSave);
        File.WriteAllText(saveFilePath, jsonSave);
    }
   
}