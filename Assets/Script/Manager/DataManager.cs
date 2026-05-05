using System.IO;
using UnityEngine;
[DefaultExecutionOrder(-10)]
public class DataManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static DataManager Instance;

    private PlayerData playerData;

    private string playerPath;
    private void Awake()
    {
        Instance = this;
        LoadDataPlayer();
    }

    private void LoadDataPlayer()
    {
        playerPath = Path.Combine(Application.persistentDataPath, "player_data.json");
        if (File.Exists(playerPath))
        {
            string json = File.ReadAllText(playerPath);
            playerData = JsonUtility.FromJson<PlayerData>(json);

            if(playerData == null)
            {
                CreateNewFile();
            }
        }
        else
        {
            CreateNewFile();
        }
    }

    public PlayerData getPlayerData()
    {
        if (playerData == null)
        {
            LoadDataPlayer();
        }
        return playerData;
    }
    public void SavePlayerData(PlayerData newData)
    {
        playerData = newData;
        SaveToJson();
    }
    public void SaveToJson()
    {
        if (playerData != null)
        {
            string json = JsonUtility.ToJson(playerData);
            File.WriteAllText(playerPath, json);
            Debug.Log("Đã lưu dl người chơi");
        }
    }
    public void CreateNewFile()
    {
        playerData = new PlayerData(1,true,true,true,10,0);
        SaveToJson();
    }
}
