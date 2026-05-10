using UnityEngine;

public class BrickController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject brick;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.Instance.PickUpBrick();
            brick.SetActive(false);
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    private void OnEnable()
    {
        gameObject.GetComponent<Collider>().enabled = true;
        brick.SetActive(true);
    }
}
