using System.Collections;
using TMPro;
using UnityEngine;

public class WinUI : MonoBehaviour
{
 
    [SerializeField] private TextMeshProUGUI stackCount;
    [SerializeField] private PlayerController player;
    [SerializeField] private GridManager grid;

    private int totalStack;

    private void OnEnable()
    {
        totalStack = grid.stackCount;
        UpdateUI();
    }

    public void OnRetryClick()
    {
        GameManager.instance.RestartLevel();
    }

    public void OnMainMenuClick()
    {
        GameManager.instance.ChangeState(GameManager.GameState.Main);
    }

    public void OnNextLevelClick()
    {

        WaitForCollectedCoin(2f);
    }

    IEnumerator WaitForCollectedCoin(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.instance.NextLevel();
    }
    public void UpdateUI()
    {
        stackCount.text = "Stacks:\n" + player.brickCount + " / " + totalStack;
    }
}
