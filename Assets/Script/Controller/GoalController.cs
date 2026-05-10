using System.Collections;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireWork1;
    [SerializeField] private ParticleSystem fireWork2;

    [SerializeField] private GameObject ChestClose;
    [SerializeField] private GameObject ChestOpen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        fireWork1.Stop();
        fireWork2.Stop();
        ChestClose.SetActive(true);
        ChestOpen.SetActive(false);
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
            PlayerController.Instance.ClearBrick();
            PlayerController.Instance.ChangeState(PlayerController.State.Celebrating);
            StartCoroutine(CallWinState(3f));
        }
    }

    public IEnumerator CallWinState(float time)
    {
        yield return new WaitForSeconds(time);
        GameManager.instance.ChangeState(GameManager.GameState.Win);

    }
}
