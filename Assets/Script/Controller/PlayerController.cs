using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Direction = InputManager.Direction;

public class PlayerController : MonoBehaviour
{
    public enum State
    {
        Idle,
        Jumping,
        Celebrating
    }

    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject brickPref;
    [SerializeField] private GameObject playerImage;
    [SerializeField] private Transform brickSpawnPoint;
    [SerializeField] private Animator anim;

    [SerializeField] private State currentState;
    private bool isMoving = false;
    private bool isSwitch = true;
    private float brickHeight;
    private float brickLength;

    private Vector3 targetPos; // Lưu điểm đến cuối cùng
    private Vector3 firstPos;

    public List<GameObject> collectedBricks = new List<GameObject>();
    public int brickCount;
    public Direction currentDirection;

    private void Awake()
    {
        Mesh mesh = brickPref.GetComponent<MeshFilter>().sharedMesh;
        brickHeight = mesh.bounds.size.z * brickPref.transform.localScale.z;
        brickLength = mesh.bounds.size.x * brickPref.transform.localScale.x;
    }

    private void Update()
    {
        if (!isMoving) return;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        InputManager.Instance.DeActiveInput();
        if(Vector3.Distance(transform.position, targetPos) <= brickLength/2)
        {
            isMoving = false;
            isSwitch = true;
            InputManager.Instance.ActiveInput();
        }
    }

    public void OnInit(LevelData level)
    {
        brickCount = 0;
        //vì startPos lưu là vector3(x,y,0) trong tọa độ grid nhưng trong hệ tọa độ gốc thì nó là (x,0,z)
        Vector3 v = new Vector3(level.startPos.x + level.origin.x + 0.5f, 3, level.startPos.y + level.origin.y + 0.5f);
        transform.position = v;
        firstPos = transform.position;
        UpdatePlayerHeight();
        ChangeState(State.Idle);

    }

    public void Move(Direction dir)
    {
        Debug.Log("isSwitch: " + isSwitch);
        if (!isSwitch) return;
        Debug.Log("move duoc goi");
        targetPos = SetTargetPos(dir);
        if(Vector3.Distance(transform.position,targetPos)  <= brickLength)
        {
            isMoving = false;
            Debug.Log("gap truong hop khoang cach qua gan");
            return;
        }
        if(Vector3.Distance(transform.position, targetPos) > brickLength)
        {
            isMoving = true;
            isSwitch = false;
            ChangeState(State.Jumping);
            Debug.Log("Gap truong hop thoa man");
        }
    }
    private Vector3 SetTargetPos(Direction dir)
    {
        Vector3 target;
        Vector3 direction = CheckDirection(dir);
        if(direction == Vector3.zero) return transform.position;
        RaycastHit hit;
        currentDirection = dir;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                target = hit.point;
                Debug.Log("hit target cach" + hit.distance);
                return target;
            }
            if (hit.collider.CompareTag("Goal"))
            {
                target = hit.point + direction * brickLength/2;
                Debug.Log("hit target cach" + hit.distance);
                return target;
            }
        }
        return transform.position;
    }
    public Vector3 CheckDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.Left:
                return Vector3.left;
            case Direction.Right:
                return Vector3.right;
            case Direction.Forward:
                return Vector3.forward;
            case Direction.Back:
                return Vector3.back;
        }
        return Vector3.zero;
    }
    public void PickUpBrick()
    {
        float posY = brickHeight * collectedBricks.Count;
        Vector3 position = new Vector3(0,posY,0);
        GameObject brick = SimplePool.Instance.Spawn(brickPref, position, brickPref.transform.rotation, brickSpawnPoint);
        collectedBricks.Add(brick);
        brickCount++;
        UpdatePlayerHeight();
    }

    public void DropBrick()
    {
        if (collectedBricks.Count <= 0)
        {
            isMoving = false;
            return;
        }
        SimplePool.Instance.Despawn(collectedBricks[collectedBricks.Count - 1]);
        collectedBricks.RemoveAt(collectedBricks.Count - 1);
        UpdatePlayerHeight();
    }
    public void ClearBrick()
    {
        if (collectedBricks.Count <= 0) return;
        for(int i = 0;i < collectedBricks.Count; i++)
        {
            SimplePool.Instance.Despawn(collectedBricks[i]);
        }
        collectedBricks.Clear();
        UpdatePlayerHeight();
    }

    void UpdatePlayerHeight()
    {
        float offSet = brickHeight * (collectedBricks.Count-1);
        playerImage.transform.localPosition = new Vector3(0,offSet,0);
    }

    //các hàm animation
    public void ChangeState(State state)
    {
        currentState = state;
        switch (state)
        {
            case State.Idle:
                anim.SetInteger("action", 0);
                break;
            case State.Jumping:
                anim.SetInteger("action", 1);
                StartCoroutine(ReturnToIdleAfterDelay(0.18f)); // Giả sử nhảy mất 0.5s
                break;
            case State.Celebrating:
                anim.SetInteger("action", 2);
                break;
        }
    }

    public IEnumerator ReturnToIdleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ChangeState(State.Idle);
    }

    private void OnDrawGizmos()
    {
        // 1. Vẽ tia Raycast đang bắn ra theo 4 hướng để kiểm tra va chạm (Chỉ vẽ trong Editor)
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position , Vector3.forward * 5f);
        Gizmos.DrawRay(transform.position , Vector3.back * 5f);
        Gizmos.DrawRay(transform.position, Vector3.left * 5f);
        Gizmos.DrawRay(transform.position , Vector3.right * 5f);

        // 2. Vẽ điểm đến cuối cùng (Target Position)
        if (targetPos != Vector3.zero)
        {
            Gizmos.color = Color.red;
            // Vẽ một khối cầu nhỏ tại điểm đích
            Gizmos.DrawSphere(targetPos, 0.3f);

            // Vẽ đường thẳng từ người chơi đến điểm đích
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, targetPos);
        }
    }


}