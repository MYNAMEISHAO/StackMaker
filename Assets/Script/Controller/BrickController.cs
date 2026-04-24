using UnityEngine;

public class BrickController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private PlayerController playerController;
    private void Awake()
    {
        playerController = GameObject.FindAnyObjectByType<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.PickUpBrick();
            transform.gameObject.SetActive(false);
        }
    }
}
