using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObject : MonoBehaviour
{
    private Material mat;
    private Vector3[] vertices = new Vector3[4];

    private void Start()
    {
        mat = GetComponent<MeshRenderer>().material;

        vertices[0] = (new Vector3(transform.position.x + transform.localScale.x / 2, 0, transform.position.z + transform.localScale.z / 2));
        vertices[1] = (new Vector3(transform.position.x - transform.localScale.x / 2, 0, transform.position.z + transform.localScale.z / 2));
        vertices[2] = (new Vector3(transform.position.x + transform.localScale.x / 2, 0, transform.position.z - transform.localScale.z / 2));
        vertices[3] = (new Vector3(transform.position.x - transform.localScale.x / 2, 0, transform.position.z - transform.localScale.z / 2));
    }

    public bool isWallFrontOfPlayer(DirectionState state, Vector3 playerPosition)
    {
        switch (state)
        {
            case DirectionState.NORTH:
                return CheckVertices(false, false, playerPosition.z);

            case DirectionState.EAST:
                return CheckVertices(true, false, playerPosition.x);

            case DirectionState.SOUTH:
                return CheckVertices(false, true, playerPosition.z);

            case DirectionState.WEST:
                return CheckVertices(true, true, playerPosition.x);
        }
        return false;
    }

    private bool CheckVertices(bool isX, bool isUp, float pos)
    {
        foreach (Vector3 vertex in vertices)
        {
            if (isX)
            {
                if (isUp)
                {
                    if (vertex.x < pos)
                        return false;
                }
                else
                {
                    if (vertex.x > pos)
                        return false;
                }
            }
            else
            {
                if (isUp)
                {
                    if (vertex.z < pos)
                        return false;
                }
                else
                {
                    if (vertex.z > pos)
                        return false;
                }
            }
        }

        return true;
    }
}
