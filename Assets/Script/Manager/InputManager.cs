using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    [SerializeField] private float minSwipeDistance = 50f;

    [SerializeField] private GameObject player;
    Direction direction;
    bool isActive= true;

    private void Awake()
    {
        Instance = this;
        isActive = true;
    }

    void Update()
    {
        if(isActive)
            HandleSwipe();
    }

    void HandleSwipe()
    {
        if (Input.GetMouseButtonDown(0)) startTouchPosition = Input.mousePosition;
        if (Input.GetMouseButtonUp(0))
        {
            endTouchPosition = Input.mousePosition;
            DetectSwipe();
        }
    }
    void DetectSwipe()
    {
        if (GameManager.instance.currentState == GameManager.GameState.Main)
        {
            GameManager.instance.ChangeState(GameManager.GameState.Play);
        }

        Vector2 swipeVector = endTouchPosition - startTouchPosition;

        if (swipeVector.magnitude > minSwipeDistance)
        {

            if (Mathf.Abs(swipeVector.x) > Mathf.Abs(swipeVector.y))
                direction = (swipeVector.x > 0) ? Direction.Right : Direction.Left;
            else
                direction = (swipeVector.y > 0) ? Direction.Forward : Direction.Back;

            player.GetComponent<PlayerController>().Move(direction);
        }
    }
    public void ActiveInput()
    {
        isActive = true;
    }
    public void DeActiveInput()
    {
        isActive = false;
    }

    public enum Direction
    {
        Left,
        Right,
        Forward,
        Back
    }
}