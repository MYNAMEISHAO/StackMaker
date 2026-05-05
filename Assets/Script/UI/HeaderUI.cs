using TMPro;
using UnityEngine;

public class HeaderUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI gemText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        coinText.text = DataManager.Instance.getPlayerData().getCoin().ToString();
        gemText.text = DataManager.Instance.getPlayerData().getGem().ToString();
    }

    public void OnSettingClick()
    {
        GameManager.instance.ChangeState(GameManager.GameState.Pause);
    }

}
