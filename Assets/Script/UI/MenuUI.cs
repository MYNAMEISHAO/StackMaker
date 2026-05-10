using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject playImage;

    private void Start()
    {
        UpdateUI();
    }
    private void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        levelText.text = "Level " + DataManager.Instance.getPlayerData().getLevel();
    }
}
