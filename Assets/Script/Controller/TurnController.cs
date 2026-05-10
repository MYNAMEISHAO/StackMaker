using UnityEngine;
using Direction = InputManager.Direction;

public class TurnController : MonoBehaviour
{
    [SerializeField] private Direction dir1;
    [SerializeField] private Direction toDir1;
    [SerializeField] private Direction dir2;
    [SerializeField] private Direction toDir2;

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject brick;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered turn controller");
            if (PlayerController.Instance.currentDirection == dir1)
            {
                PlayerController.Instance.Move(toDir1);
            }
            else if (PlayerController.Instance.currentDirection == dir2)
            {
                PlayerController.Instance.Move(toDir2);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            animator.SetInteger("turn", 1);
        PlayerController.Instance.PickUpBrick();
        brick.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            animator.SetInteger("turn", 0);
    }

    private void OnEnable()
    {
        brick.SetActive(true);
    }

}


