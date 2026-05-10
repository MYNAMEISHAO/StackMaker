using System.Collections;
using TMPro;
using UnityEngine;

public class LoseUI : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private GridController grid;

    [SerializeField] private TextMeshProUGUI stackCountText;
    [SerializeField] private FaderUI faderUI;


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
        faderUI.Transition(() =>
        {
            GameManager.instance.RestartLevel();
        }, 0.5f);
    }

    public void OnMenuClick()
    {
        faderUI.Transition(() =>
        {
            GameManager.instance.ChangeState(GameManager.GameState.Main);
        }, 0.5f);
    }
}
