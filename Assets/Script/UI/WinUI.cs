using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class WinUI : MonoBehaviour
{
    [SerializeField] private GameObject coinImage;
    [SerializeField] private GameObject coinPref;

    [SerializeField] private Transform coinStart;
    [SerializeField] private Transform coinEnd;

    [SerializeField] private float coinMoveDuration = 1f;
    [SerializeField] private Ease coinMoveEase;


    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private FaderUI faderUI;
    private void OnEnable()
    {
        coinText.text = DataManager.Instance.getPlayerData().getCoin().ToString();
        coinImage.SetActive(true);
    }

    public void OnRetryClick()
    {
        faderUI.Transition(() =>
        {
            GameManager.instance.RestartLevel();
        }, 0.5f);
    }

    public void OnNextLevelClick()
    {
        SpawnCoins();
        coinImage.SetActive(false);
        StartCoroutine(WaitForCoin());
    }

    public IEnumerator WaitForCoin()
    {
        yield return new WaitForSeconds(3f);
        faderUI.Transition(() =>
        {
            GameManager.instance.NextLevel();
        }, 0.5f);
    }

    public void UpdateUI()
    {
        coinText.text = DataManager.Instance.getPlayerData().getCoin().ToString();
    }


    public void SpawnCoins()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject coin = SimplePool.Instance.Spawn(coinPref, coinStart.localPosition, Quaternion.identity, transform);
            var offset = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0);
            var startPos = coinStart.transform.position + offset;

            coin.transform.DOMove(startPos, coinMoveDuration).SetEase(coinMoveEase);
            coin.transform.DOMove(coinEnd.position, coinMoveDuration).SetEase(coinMoveEase).OnComplete(() => CompleteMove(coin)).SetDelay(1f);
        }
    }

    public void CompleteMove(GameObject gameObject)
    {
        SimplePool.Instance.Despawn(gameObject);
        int currentCoin = int.Parse(coinText.text);
        DataManager.Instance.AddCoin();
        UpdateUI();
    }
}
