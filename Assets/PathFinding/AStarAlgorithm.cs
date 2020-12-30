using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class AStarAlgorithm : MonoBehaviour
{
    
    public AStarZone AStarZone;

    public Stack<GridNode> SearchPath(GridNode from, GridNode to)
    {
        List<GridNode> Nodes = new List<GridNode>();
        
        List<GridNode> VisitedNodes = new List<GridNode>();

        float g = 0;
        Nodes.Add(from);
        
        GridNode currentNode = null;

        while (Nodes.Count > 0)
        {
            float lowest = Nodes.Min(l => l.F);

            currentNode = Nodes.FirstOrDefault(l => l.F == lowest);

            VisitedNodes.Add(currentNode);

            Nodes.Remove(currentNode);
            
            if (VisitedNodes.FirstOrDefault(l => l.Position == to.Position) != null) break;
            
            List<GridNode> WalkableNodes = AStarZone.GetNeighboringNodes(currentNode);
            g++;
            foreach (GridNode gridNode in WalkableNodes)
            {
                if(VisitedNodes.FirstOrDefault(l => l.Position == gridNode.Position) != null) continue;

                if (Nodes.FirstOrDefault(l => l.Position == gridNode.Position) == null)
                {
                    gridNode.G = g;
                    gridNode.H = ComputeHScore(gridNode, to);
                    gridNode.F = gridNode.G + gridNode.H;
                    gridNode.ParentNode = currentNode;
                    
                    Nodes.Insert(0, gridNode);
                } else {
                    if (g + gridNode.H < gridNode.F)
                    {
                        gridNode.G = g;
                        gridNode.F = gridNode.G + gridNode.H;
                        gridNode.ParentNode = currentNode;
                    }
                }
            }
        }

        Stack<GridNode> path = new Stack<GridNode>();
        while (currentNode != null)
        {
            path.Push(currentNode);
            currentNode = currentNode.ParentNode;
        }
        
        return path;
    }

    private float ComputeHScore(GridNode node, GridNode target)
    {
        return Vector3.Distance(node.Position, target.Position);
    }
}