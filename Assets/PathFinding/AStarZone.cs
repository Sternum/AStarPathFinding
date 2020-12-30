using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class AStarZone : MonoBehaviour
{
    
    public float Width = 10;
    public float Height = 10;
    [Range(0.1f, 10.0f)]
    public float Step = 0.25f;

    public LayerMask layers;
    
    [SerializeField]
    public List<GridNode> WalkableNodes;
    
    void Start()
    {
        WalkableNodes = new List<GridNode>();
        CheckWalkableArea();
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, 1, Height));
        
        float widthOffset = Width / 2;
        float heightOffset = Height / 2;
        Vector3 pos = transform.position;
        Vector3 firstCheck = transform.position - new Vector3(widthOffset, 0, heightOffset);
        Vector3 lastCheck = transform.position + new Vector3(widthOffset, 0, heightOffset);

        float startZindex = firstCheck.z;
        while (firstCheck.x < lastCheck.x)
        {
            while (firstCheck.z < lastCheck.z)
            {
                RaycastHit hit;
                //Gizmos.DrawRay(firstCheck, Vector3.down * 10);
                if (Physics.Raycast(firstCheck, Vector3.down, out hit, 30.0f, layers))
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.CompareTag("Walkable"))
                        {
                            Gizmos.color = Color.cyan;
                        }
                       Gizmos.DrawSphere(hit.point, 0.1f);
                    }
                }
                firstCheck = firstCheck + new Vector3(0, 0, Step);
            }
            firstCheck = firstCheck + new Vector3(Step, 0, 0);
            firstCheck.z = startZindex;
        }
    }
    private void CheckWalkableArea()
    {
        float widthOffset = Width / 2;
        float heightOffset = Height / 2;
        Vector3 pos = transform.position;
        Vector3 firstCheck = transform.position - new Vector3(widthOffset, 0, heightOffset);
        Vector3 lastCheck = transform.position + new Vector3(widthOffset, 0, heightOffset);

        float startZindex = firstCheck.z;
        while (firstCheck.x < lastCheck.x)
        {
            while (firstCheck.z < lastCheck.z)
            {
                RaycastHit hit;
                if (Physics.Raycast(firstCheck, Vector3.down, out hit, 30.0f, layers))
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.CompareTag("Walkable"))
                        {
                            WalkableNodes.Add(new GridNode(hit.point, true));
                        }
                        else
                        {
                            WalkableNodes.Add(new GridNode(hit.point, false));
                        }
                      
                    }
                }
                firstCheck = firstCheck + new Vector3(0, 0, Step);
            }
            firstCheck = firstCheck + new Vector3(Step, 0, 0);
            firstCheck.z = startZindex;
        }
    }

    public GridNode GetClosestNode(Vector3 origin)
    {
        float closetDist = WalkableNodes.Min(l => Vector3.Distance(origin, l.Position));
        return WalkableNodes.Find(l => closetDist == Vector3.Distance(origin, l.Position));
    }
    
    public List<GridNode> GetNeighboringNodes(GridNode currentNode)
    {
        GridNode node = WalkableNodes.Find(n => n.Position == currentNode.Position);
        List<GridNode> NeighboringNodes = new List<GridNode>();

        GridNode foundNode =
            WalkableNodes.Find(gridNode => gridNode.Position == currentNode.Position + new Vector3(Step, 0, 0));
        if(foundNode != null && foundNode.Walkable) NeighboringNodes.Add(foundNode);
        
        foundNode = WalkableNodes.Find(gridNode => gridNode.Position == currentNode.Position + new Vector3(Step, 0, Step));
        if(foundNode != null && foundNode.Walkable) NeighboringNodes.Add(foundNode);
        
        foundNode = WalkableNodes.Find(gridNode => gridNode.Position == currentNode.Position + new Vector3(0, 0, Step));
        if(foundNode != null && foundNode.Walkable) NeighboringNodes.Add(foundNode);
        
        foundNode = WalkableNodes.Find(gridNode => gridNode.Position == currentNode.Position + new Vector3(-Step, 0, 0));
        if(foundNode != null && foundNode.Walkable) NeighboringNodes.Add(foundNode);

        foundNode = WalkableNodes.Find(gridNode => gridNode.Position == currentNode.Position + new Vector3(0, 0, -Step));
        if(foundNode != null && foundNode.Walkable) NeighboringNodes.Add(foundNode);
        
        foundNode = WalkableNodes.Find(gridNode => gridNode.Position == currentNode.Position + new Vector3(-Step, 0, -Step));
        if(foundNode != null && foundNode.Walkable) NeighboringNodes.Add(foundNode);
        
        foundNode = WalkableNodes.Find(gridNode => gridNode.Position == currentNode.Position + new Vector3(-Step, 0, Step));
        if(foundNode != null && foundNode.Walkable) NeighboringNodes.Add(foundNode);
        
        foundNode = WalkableNodes.Find(gridNode => gridNode.Position == currentNode.Position + new Vector3(Step, 0, -Step));
        if(foundNode != null && foundNode.Walkable) NeighboringNodes.Add(foundNode);
        
        return NeighboringNodes;
    }
}
