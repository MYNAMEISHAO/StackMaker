using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
[DefaultExecutionOrder(-5)]
public class LevelManager : MonoBehaviour
{
    [SerializeField] private GridManager grid;
    [SerializeField] private PlayerController player;
    public static LevelManager Instance;
    private string levelPath;
    private LevelDataBase levelDataBase;

    private void Awake()
    {
        Instance = this;
        OnInit();
    }

    public void OnInit()
    {
        LoadAllLevel();
    }

    private void LoadAllLevel()
    {
        levelPath = Path.Combine(Application.dataPath, "level_data.json");
        string json = File.ReadAllText(levelPath);
        levelDataBase = JsonUtility.FromJson<LevelDataBase>(json);
        SortingLevel();
    }

    public void OnLoadLevel(int level)
    {
        if(level < 1 || level > levelDataBase.levels.Count)
        {
            Debug.LogError("Level " + level + " không tồn tại trong cơ sở dữ liệu.");
            return;
        }
        int currentLevel = level - 1;
        LevelData Level = levelDataBase.levels[currentLevel];
        Debug.Log(Level);
        grid.OnInit(Level);
        player.OnInit(Level);
    }

    private void SortingLevel()
    {
        levelDataBase.levels.Sort((level1, level2) => 
            GetLevelNumber(level1.levelName)
            .CompareTo(GetLevelNumber(level2.levelName)));
    }

    private int GetLevelNumber(string name)
    {
        // Dùng Regex để lấy tất cả các chữ số trong tên
        string result = Regex.Match(name, @"\d+").Value;
        if (int.TryParse(result, out int number))
        {
            return number;
        }
        return 0; // Trả về 0 nếu không tìm thấy số
    }

    public void OnDespawn()
    {
        grid.ClearGrid();
    }
}
