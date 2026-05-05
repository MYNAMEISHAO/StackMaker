using UnityEngine;
using Direction = InputManager.Direction;

public class TurnController : MonoBehaviour
{
    [SerializeField] private Direction dir1;
    [SerializeField] private Direction toDir1;
    [SerializeField] private Direction dir2;
    [SerializeField] private Direction toDir2;

    [SerializeField] private Animator animator;

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered turn controller");
            if (other.GetComponent<PlayerController>().currentDirection == dir1)
            {
                other.GetComponent<PlayerController>().Move(toDir1);
            }
             else if (other.GetComponent<PlayerController>().currentDirection == dir2)
            {
                other.GetComponent<PlayerController>().Move(toDir2);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            animator.SetInteger("turn", 1);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            animator.SetInteger("turn", 0);
    }
}


