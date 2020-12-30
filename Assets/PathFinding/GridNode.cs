using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode 
{
    public Vector3 Position;
    public float F;
    public float G;
    public float H;
    public bool Walkable;
    
    public GridNode ParentNode;

    public GridNode(Vector3 position, bool walkable)
    {
        Position = position;
        Walkable = walkable;
    }
}
