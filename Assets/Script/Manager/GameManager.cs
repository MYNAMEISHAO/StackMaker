using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static GameManager instance;
    public static GameState currentState;
    private void Awake()
    {
        instance = this;
        OnInit();
    }

    void OnInit()
    {
        ChangeState(GameState.Main);
    }
    public void ChangeState(GameState state)
    {
        currentState = state;
        switch (currentState)
        {
            case GameState.Main:
                HandleMain();
                break;
            case GameState.Play:
                HandlePlay();
                break;
            case GameState.Pause:
                HandlePause();
                break;
            case GameState.Win:
                HandleWin();
                break;
            case GameState.Lose:
                HandleLose();
                break;
        }
    }

    private void HandleLose()
    {

    }

    private void HandleWin()
    {
        UIManager.Instance.OpenWinUI();
    }

    private void HandlePause()
    {
        UIManager.Instance.OpenSettingUI();
    }

    private void HandlePlay()
    {
        UIManager.Instance.CloseMenuUI();
        UIManager.Instance.CloseWinUI();
        UIManager.Instance.CloseSettingUI();


    }

    private void HandleMain()
    {
        int currentLevel = DataManager.Instance.getPlayerData().getLevel();
        Debug.Log("level load la" + currentLevel);
        LevelManager.Instance.OnLoadLevel(currentLevel);

        UIManager.Instance.OpenMenuUI();
        UIManager.Instance.OpenHeaderUI();
    }

    public void RestartLevel()
    {
        int currentLevel = DataManager.Instance.getPlayerData().getLevel();
        Debug.Log("level load la" + currentLevel);
        LevelManager.Instance.OnLoadLevel(currentLevel);
    }

    public void NextLevel()
    {
        int currentLevel = DataManager.Instance.getPlayerData().getLevel();
        currentLevel++;
        DataManager.Instance.getPlayerData().setLevel(currentLevel);
        DataManager.Instance.SaveToJson();
        LevelManager.Instance.OnLoadLevel(currentLevel);
    }
    // Update is called once per frame
    public enum GameState
    {
        Main,
        Play,
        Win,
        Lose,
        Pause
    }
}
