using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public AStarZone AStarZone;

    public AStarAlgorithm AStarAlgorithm;

    public LayerMask LayerMask;

    public GameObject Player;

    public CharacterMovement CharacterMovement;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, LayerMask))
            {
                if (hit.collider != null && hit.collider.CompareTag("Walkable"))
                {
                    GridNode targetNode = AStarZone.GetClosedNode(hit.point);
                    GridNode playerNode = AStarZone.GetClosedNode(Player.transform.position);
                    Stack<GridNode> path = AStarAlgorithm.SearchPath(playerNode, targetNode);
                    CharacterMovement.SetPath(path);
                }
            }
        }
    }
}
