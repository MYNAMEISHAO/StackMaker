using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform WallTile;
    [SerializeField] private Transform BridgeTile;
    [SerializeField] private Transform BrickTile;
    [SerializeField] private Transform GoalTile;

    [SerializeField] private GameObject Bridge;
    [SerializeField] private GameObject Brick;
    [SerializeField] private GameObject Wall;
    [SerializeField] private GameObject Goal;

    private int width, length;
    private Vector3 origin;

    public void OnInit(LevelData level)
    {
        width = level.width;
        length = level.length;
        int index = 0;
        for (int y = 0; y < length; y++)
        {
            for(int x = 0; x < width; x++)
            {
                Block block = level.gridData[index];
                int val = block.type;
                Quaternion rotation = block.rotation;
                Vector3 pos = CalculatePos(x, 0, y, 1, 1, 1);
                if (val == 2)
                {
                    SimplePool.Instance.Spawn(Bridge,pos,block.rotation, BridgeTile);
                }
                else if (val == -1)
                {
                    SimplePool.Instance.Spawn(Wall, pos,block.rotation, WallTile);
                }
                else if (val == 1)
                {
                    SimplePool.Instance.Spawn(Brick, pos,block.rotation, BrickTile);
                }
                else if(val == -2)
                {
                    SimplePool.Instance.Spawn(Bridge, pos,block.rotation ,BridgeTile);
                }
                else if(val == 3)
                {
                    SimplePool.Instance.Spawn(Goal, pos, block.rotation, GoalTile);
                }
                index++;
            }
        }
    }

    private Vector3 CalculatePos(int x,int y, int z, int length, int width, int height)
    {
        return new Vector3(x *  width, y * height, z * length);
    }
    public void ClearGrid()
    {
        
    }
   
}
