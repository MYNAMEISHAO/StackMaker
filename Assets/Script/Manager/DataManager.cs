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

    public void AddGem()
    {
        if (playerData != null)
        {
            playerData.setGem(playerData.getGem() + 10);
        }
    }

    public void AddCoin()
    {
        if (playerData != null)
        {
            playerData.setCoin(playerData.getCoin() + 4);
        }
    }

    public void SetSound(bool isSoundOn)
    {
        if (playerData != null)
        {
            playerData.setSoundOn(isSoundOn);
        }
    }

    public void SetMusic(bool isMusicOn)
    {
        if (playerData != null)
        {
            playerData.setMusicOn(isMusicOn);
        }
    }

    public void SetShake(bool isShakeOn)
    {
        if (playerData != null)
        {
            playerData.setShakeOn(isShakeOn);
        }
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
