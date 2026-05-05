using System;
using UnityEngine;
[Serializable]
public class Block
{
    public int type;
    public Quaternion rotation;
    public Vector3Int position;

    public Block()
    {
    }

    public Block(int type, Quaternion rotation, Vector3Int position)
    {
        this.type = type;
        this.rotation = rotation;
        this.position = position;
    }
}
