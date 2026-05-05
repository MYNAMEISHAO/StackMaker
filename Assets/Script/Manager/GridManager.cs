using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [System.Serializable]
    public struct BlockConfigs
    {
        public GameObject pref;
        public int type;
        public Transform parent;
    }

    [Header("Mappings")]
    [SerializeField] private List<BlockConfigs> blocks;
    private Dictionary<int,BlockConfigs> blockMappings = new Dictionary<int, BlockConfigs>();

    private int width, length;
    private Vector3 origin;
    private List<GameObject> spawnedBlocks = new List<GameObject>();

    public int stackCount = 0;
    public void OnInit(LevelData level)
    {
        stackCount = 0;
        origin = level.origin;
        //Nạp dữ liệu vào Dictionary cho dễ tìm
        foreach (var configs in blocks){
            if (!blockMappings.ContainsKey(configs.type))
            {
                blockMappings.Add(configs.type, configs);
            }
        }

        //Lấy data level
        width = level.width;
        length = level.length;
        int index = 0;
        
        foreach (var block in level.gridData)
        {
            int val = block.type;
            Quaternion rotation = block.rotation;
            Vector3 pos = CalculatePos(block.position.x, 0, block.position.y, 1, 1, 1);

            if (blockMappings.TryGetValue(val, out BlockConfigs blockConfigs))
            {
                GameObject go = SimplePool.Instance.Spawn(blockConfigs.pref, pos, rotation, blockConfigs.parent);
                spawnedBlocks.Add(go);
                if (blockConfigs.type >= 1 && blockConfigs.type <= 5) stackCount++;
            }
            index++;
        }
    }

    private Vector3 CalculatePos(int x,int y, int z, int length, int width, int height)
    {
        return new Vector3((x + origin.x) *  width + 0.5f, y * height, (z + origin.z) * length + 0.5f);
    }
    public void ClearGrid()
    {
        foreach(var block in spawnedBlocks)
        {
            SimplePool.Instance.Despawn(block);
        }
    }
   
}
