using TMPro;
using UnityEngine;

public class LoseUI : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private GridController grid;

    [SerializeField] private TextMeshProUGUI stackCountText;

    private int stackCount;
    private int totalStack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void UpdateUI()
    {
        stackCount = player.brickCount;
        totalStack = grid.stackCount;
        stackCountText.text = "Stacks:\n" + stackCount + "/" + totalStack;
    }

    public void OnRetryClick()
    {
        GameManager.instance.RestartLevel();
    }

    public void OnMenuClick()
    {
        GameManager.instance.ChangeState(GameManager.GameState.Main);
    }
}
