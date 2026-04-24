using System;
using UnityEngine;
[Serializable]
public class Block
{
    public int type;
    public Quaternion rotation;

    public Block()
    {
    }

    public Block(int type, Quaternion rotation)
    {
        this.type = type;
        this.rotation = rotation;
    }
}
