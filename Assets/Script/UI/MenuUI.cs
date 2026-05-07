using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private TextMeshProUGUI levelText;

    private void OnEnable()
    {
        UpdateUI();
    }

    public void OnClickPlay()
    {
        GameManager.instance.ChangeState(GameManager.GameState.Play);
    }

    public void UpdateUI()
    {
        levelText.text = "Level " + DataManager.Instance.getPlayerData().getLevel();
    }

}
