using UnityEngine;

public class SettingUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject soundButton;
    [SerializeField] private GameObject musicButton;
    [SerializeField] private GameObject shakeButton;

    private bool isSoundOn = true;
    private bool isMusicOn = true;
    private bool isShakeOn = true;

    private void OnEnable()
    {
        isSoundOn = DataManager.Instance.getPlayerData().isSoundOn();
        isMusicOn = DataManager.Instance.getPlayerData().isMusicOn();
        soundButton.transform.GetChild(0).gameObject.SetActive(isSoundOn);
    }
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

    public void OnSoundClick()
    {
        isSoundOn = !isSoundOn;
        soundButton.transform.GetChild(0).gameObject.SetActive(isSoundOn);
        soundButton.transform.GetChild(1).gameObject.SetActive(!isSoundOn);
    }

    public void OnMusicClick()
    {
        isMusicOn = !isMusicOn;
        musicButton.transform.GetChild(0).gameObject.SetActive(isMusicOn);
        musicButton.transform.GetChild(1).gameObject.SetActive(!isMusicOn);
    }

    public void OnShakeClick()
    {
        isShakeOn = !isShakeOn;
        shakeButton.transform.GetChild(0).gameObject.SetActive(isShakeOn);
        shakeButton.transform.GetChild(1).gameObject.SetActive(!isShakeOn);
    }

    public void UpdateUI()
    {

    }
}
