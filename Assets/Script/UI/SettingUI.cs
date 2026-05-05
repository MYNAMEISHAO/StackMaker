using UnityEngine;

public class SettingUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject soundButton;
    [SerializeField] private GameObject musicButton;
    [SerializeField] private GameObject shakeButton;
    public void OnRetryClick()
    {
        GameManager.instance.RestartLevel();
        GameManager.instance.ChangeState(GameManager.GameState.Play);
    }

    public void OnMainMenuClick()
    {
        GameManager.instance.ChangeState(GameManager.GameState.Main);
    }

    public void OnExitClick()
    {
        UIManager.Instance.CloseSettingUI();
        GameManager.instance.ChangeState(GameManager.GameState.Play);
    }

    public void OnResumeClick()
    {
        UIManager.Instance.CloseSettingUI();
        GameManager.instance.ChangeState(GameManager.GameState.Play);
    }
    public void UpdateUI()
    {

    }
}
