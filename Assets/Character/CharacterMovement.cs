using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CharacterMovement : MonoBehaviour
{

    private Stack<GridNode> Path;

    private void Start()
    {
        Path = new Stack<GridNode>();
    }

    public void SetPath(Stack<GridNode> path)
    {
        Path = path;
    }
    
    private void Update()
    {
        if (Path.Count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, Path.Peek().Position, Time.deltaTime * 5) ;
            if (Vector3.Distance(transform.position, Path.Peek().Position) < 0.2)
            {
                Path.Pop().ParentNode = null;
            }
        }
    }
}
