using DG.Tweening;
using UnityEngine;

public class GemController : MonoBehaviour
{
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform parent;
    [SerializeField] private float moveDuration = 1f;
    [SerializeField] private Ease moveEase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (DataManager.Instance != null)
            {
                int currentGem = DataManager.Instance.getPlayerData().getGem();
                DataManager.Instance.AddGem();
                UIManager.Instance.UpdateUI();
            }
            SimplePool.Instance.Despawn(gameObject);
        }
    }
}

