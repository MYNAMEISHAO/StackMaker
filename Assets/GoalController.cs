using UnityEngine;

public class GoalController : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireWork1;
    [SerializeField] private ParticleSystem fireWork2;

    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject ChestClose;
    [SerializeField] private GameObject ChestOpen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        player = GameObject.FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayVictory()
    {
        fireWork1.Play();
        fireWork2.Play();
        ChestClose.SetActive(false);
        ChestOpen.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayVictory();
            player.ClearBrick();
        }
    }
}
