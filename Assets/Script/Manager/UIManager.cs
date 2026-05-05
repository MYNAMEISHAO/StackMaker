using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject settingUI;
    [SerializeField] private GameObject headerUI;
    [SerializeField] private GameObject winUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        OnInit();
    }

    private void OnInit()
    {
        Instance = this;
    }
    // Update is called once per frame
    public void OpenMenuUI()
    {
        if (menuUI != null)
            menuUI.gameObject.SetActive(true);
    }

    public void CloseMenuUI()
    {
        if (menuUI != null)
            menuUI.gameObject.SetActive(false);
    }

    public void OpenHeaderUI()
    {
        headerUI.gameObject.SetActive(true);
    }
    public void CloseHeaderUI()
    {
        headerUI.gameObject.SetActive(false);
    }

    public void OpenSettingUI()
    {
        settingUI.gameObject.SetActive(true);
    }
    
    public void CloseSettingUI()
    {
        settingUI.gameObject.SetActive(false);
    }

    public void OpenWinUI()
    {
        winUI.gameObject.SetActive(true);
    }
    public void CloseWinUI()
    {
        winUI.gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        if (headerUI.activeInHierarchy)
        {
            headerUI.GetComponent<HederUI>().UpdateUI();
        }
        if (menuUI.activeInHierarchy)
        {
            menuUI.GetComponent<MenuUI>().UpdateUI();
        }
        if (settingUI.activeInHierarchy)
        {
            settingUI.GetComponent<SettingUI>().UpdateUI();
        }
    }
}
