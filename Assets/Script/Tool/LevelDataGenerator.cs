using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Windows;

public class LevelDataGenerator : MonoBehaviour
{

    [Header("Setup")]
    [SerializeField] private Grid grid;
    [SerializeField] private List<Tilemap> tilemap;
    [SerializeField] private PlayerController playerController;

    [Header("Dependencies")]
    [SerializeField] private GridManager gridManager;
    [SerializeField] private SimplePool simplePool;

    [Header("Settings")]
    [SerializeField] public string levelSaveName;
    [SerializeField] private Transform playerStart;
    [SerializeField] private bool confirmSaveFix; // Checkbox xác nhận

    //Các hàm dành cho chức năng lưu từ sence sang dữ liệu
    public void SaveLevel()
    {
        LevelData data = ConvertSceneToData();
        if (data == null) return;

        LevelDataBase db = LevelIO.LoadDatabase();

        int existingIndex = db.levels.FindIndex(l => l.levelName == data.levelName);
        if (existingIndex != -1)
        {
            if (!confirmSaveFix)
            {
                Debug.LogWarning($"Level '{data.levelName}' already exists. It will be overwritten.");
                return;
            }
            db.levels[existingIndex] = data;
            Debug.Log($"Đã ghi đè Level: {data.levelName}");

        }
        else
        {
            if(Regex.IsMatch(data.levelName, @"^Level_\d+$"))
            {
                db.levels.Add(data);
                Debug.Log("Level" + levelSaveName + " saved successfully.");
            }
            else
            {
                Debug.Log("Hãy chỉnh lại format tên level như sau: Level_ + số");
            }
            
        }
        LevelIO.SaveDatabase(db);
        confirmSaveFix = false;
    }

    private LevelData ConvertSceneToData()
    {
        BoundsInt totalBounds = GetCombinedBounds();
        if (totalBounds.size.x <= 0) return null;

        int width = totalBounds.size.x;
        int length = totalBounds.size.y;
        Vector3Int origin = totalBounds.min;
        Debug.Log(origin);

        List<Block> gridData = new List<Block>();
        foreach (var map in tilemap)
        {
            foreach (Transform child in map.transform)
            {
                Vector3Int cellPos = map.WorldToCell(child.position);
                int x = cellPos.x - origin.x;
                int y = cellPos.y - origin.y;

                if (x >= 0 && x < width && y >= 0 && y < length)
                {
                    int val = child.GetComponent<BlockEntity>().blockType;
                    gridData.Add(new Block(val, child.rotation, cellPos));
                }
            }
        }

        Vector3Int playerCellPos = grid.WorldToCell(playerStart.position);
        Debug.Log(playerCellPos);


        LevelData checkLevel = FindLevel();
        if(checkLevel!= null)
        {
            return new LevelData(checkLevel.levelName, width, length, origin, gridData, playerCellPos);
        }
        return new LevelData(levelSaveName, width, length, origin, gridData, playerCellPos);

    }

    private BoundsInt GetCombinedBounds()
    {
        Vector3Int min = new Vector3Int(int.MaxValue, int.MaxValue, 0);
        Vector3Int max = new Vector3Int(int.MinValue, int.MinValue, 0);
        bool hasData = false;

        foreach (var map in tilemap)
        {
            foreach (Transform child in map.transform)
            {
                hasData = true;
                Vector3Int pos = map.WorldToCell(child.position);
                min.x = Mathf.Min(min.x, pos.x);
                min.y = Mathf.Min(min.y, pos.y);
                max.x = Mathf.Max(max.x, pos.x);
                max.y = Mathf.Max(max.y, pos.y);
            }
        }

        if (!hasData || min.x == int.MaxValue) 
        {
            return new BoundsInt(Vector3Int.zero, Vector3Int.one);
        }
        return new BoundsInt(min.x, min.y, 0, max.x - min.x + 1, max.y - min.y + 1, 1);
    }
    //Các hàm dành cho chức năng chuyển đổi từ dữ liệu sang scene
    public void ConvertDataToScene()
    {
        LevelData levelData = FindLevel();
        if (levelData != null)
        {
            if(!CheckIsEmty())
            {
                Debug.LogWarning("Scene is not empty. Clear the scene before converting data.");
            }
            else
            {
                simplePool.OnInit();
                gridManager.OnInit(levelData);
                playerController.OnInit(levelData);
            }
        }
        else
        {
            Debug.LogWarning("Level " + levelSaveName + " not found in database. Cannot convert to scene.");
        }
    }

    public LevelData FindLevel()
    {
        string nameForm = new string(levelSaveName.Where(char.IsDigit).ToArray());
        LevelDataBase db = LevelIO.LoadDatabase();
        LevelData level = db.levels.Find(l => l.levelName.Contains(nameForm));
        if (level != null)
        {
            return level;
        }
        else
        {
            return null;
        }
    }

    //Các hàm liên quan đến việc xóa scene 
    public void ClearScene()
    {
        //simplePool.OnInit();
        foreach (var map in tilemap)
        {
            List<GameObject> children = new List<GameObject>();
            foreach (Transform child in map.transform)
            {
                children.Add(child.gameObject);
            }

            foreach(GameObject child in children)
            {
                //SimplePool.Instance.Despawn(child);
                DestroyImmediate(child);
            }
        }
    }

    private bool CheckIsEmty()
    {
        bool isEmpty = true;
        foreach (var map in tilemap)
        {
            foreach (Transform child in map.transform)
            {
                if(child.gameObject.activeInHierarchy)
                {
                    isEmpty = false;
                    break;
                }
            }
        }
        return isEmpty;
    }
}