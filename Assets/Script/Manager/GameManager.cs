using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static GameManager instance;
    public GameState currentState;
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
        UIManager.Instance.OpenLoseUI();
        InputManager.Instance.DeActiveInput();
    }

    private void HandleWin()
    {
        UIManager.Instance.OpenWinUI();
        InputManager.Instance.DeActiveInput();
    }

    private void HandlePause()
    {
        UIManager.Instance.OpenSettingUI();
        InputManager.Instance.DeActiveInput();

        PlayerController.Instance.SetMoving(false);
    }

    private void HandlePlay()
    {
        UIManager.Instance.CloseMenuUI();
        UIManager.Instance.CloseWinUI();
        UIManager.Instance.CloseSettingUI();
        UIManager.Instance.CloseLoseUI();

        InputManager.Instance.ActiveInput();
    }

    private void HandleMain()
    {
        int currentLevel = DataManager.Instance.getPlayerData().getLevel();
        Debug.Log("level load la" + currentLevel);
        LevelManager.Instance.OnLoadLevel(currentLevel);

        UIManager.Instance.OpenMenuUI();
        UIManager.Instance.OpenHeaderUI();

        UIManager.Instance.CloseWinUI();
        UIManager.Instance.CloseSettingUI();
        UIManager.Instance.CloseLoseUI();
        InputManager.Instance.ActiveInput();
    }

    public void RestartLevel()
    {
        int currentLevel = DataManager.Instance.getPlayerData().getLevel();
        Debug.Log("level load la" + currentLevel);
        LevelManager.Instance.OnLoadLevel(currentLevel);
        ChangeState(GameState.Play);
    }

    public void NextLevel()
    {
        int currentLevel = DataManager.Instance.getPlayerData().getLevel();
        currentLevel++;
        DataManager.Instance.getPlayerData().setLevel(currentLevel);
        LevelManager.Instance.OnLoadLevel(currentLevel);
        DataManager.Instance.SaveToJson();
        UIManager.Instance.UpdateUI();
        ChangeState(GameState.Play);
    }

    public void ResumeLevel()
    {
        PlayerController.Instance.SetMoving(true);
        ChangeState(GameState.Play);
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
