using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform camera;
    public Transform player;
    [SerializeField] private float offset = 5f;
    private DirectionState[] allDir = new DirectionState[4] {DirectionState.NORTH, DirectionState.EAST, DirectionState.SOUTH, DirectionState.WEST};
    private int idDir = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Left");
            camera.DOMove(GetNextPosition(1), 2);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Droite");
            camera.DOMove(GetNextPosition(-1), 2);
        }
    }

    private DirectionState GetNextDirection(int modifier)
    {
        if (idDir + modifier < 0)
            idDir = 3;
        else if (idDir + modifier > 3)
            idDir = 0;
        else
            idDir += modifier;

        return allDir[idDir];
    }

    private Vector3 GetNextPosition(int modifier)
    {
        switch (GetNextDirection(modifier))
        {
            case DirectionState.NORTH:
                return new Vector3(player.position.x, player.position.y, camera.position.z - offset);
            case DirectionState.EAST:
                return new Vector3(camera.position.x - offset, player.position.y, player.position.z);
            case DirectionState.SOUTH:
                return new Vector3(player.position.x, player.position.y, camera.position.z + offset);
            case DirectionState.WEST:
                return new Vector3(camera.position.x + offset, player.position.y, player.position.z);
        }

        return Vector3.zero;
    }
}
