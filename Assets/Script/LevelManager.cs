using System.IO;
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

    private void OnInit()
    {
        LoadAllLevel();
    }

    private void LoadAllLevel()
    {
        levelPath = Path.Combine(Application.persistentDataPath, "level_data.json");
        string json = File.ReadAllText(levelPath);
        levelDataBase = JsonUtility.FromJson<LevelDataBase>(json);
    }

    public void OnLoadLevel(int level)
    {
        int currentLevel = level - 1;
        LevelData Level = levelDataBase.levels[currentLevel];
        Debug.Log(Level);
        grid.OnInit(Level);
        player.OnInit(Level);
    }

    public void OnClearLevel()
    {

    }
}
