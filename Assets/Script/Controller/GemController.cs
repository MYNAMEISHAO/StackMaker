using UnityEngine;

public class GemController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(DataManager.Instance != null)
            {
                int currentGem = DataManager.Instance.getPlayerData().getGem();
                DataManager.Instance.AddGem();
                UIManager.Instance.UpdateUI();
            }
            SimplePool.Instance.Despawn(gameObject);
        }
    }
}
