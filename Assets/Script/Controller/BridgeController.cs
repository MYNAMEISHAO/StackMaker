using UnityEngine;

public class BridgeController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    PlayerController controller;
    private void Awake()
    {
        controller = GameObject.FindAnyObjectByType<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            controller.DropBrick();
            transform.GetChild(0).gameObject.SetActive(true);
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
