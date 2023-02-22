using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float offset = 5f;
    private DirectionState[] allDir = new DirectionState[4] {DirectionState.NORTH, DirectionState.EAST, DirectionState.SOUTH, DirectionState.WEST};
    private int idDir = 0;

    private CinemachineVirtualCamera brain;
    private CinemachineTransposer body;

    private Transform obstruction;

    private void Awake()
    {
        brain = GetComponent<CinemachineVirtualCamera>();
        body = brain.GetCinemachineComponent<CinemachineTransposer>();
        brain.Follow = player;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Debug.Log("Left");
            brain.Follow = null;
            transform.DOMove(GetNextPosition(1), 2).OnComplete(() =>
            {
                ViewObstructed();
                brain.Follow = player;
            });
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            //Debug.Log("Droite");
            brain.Follow = null;
            transform.DOMove(GetNextPosition(-1), 2).OnComplete(() =>
            {
                ViewObstructed();
                brain.Follow = player;
            });
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
                body.m_FollowOffset = new Vector3(0, 0, -offset);
                return new Vector3(player.position.x, player.position.y, transform.position.z - offset);
            
            case DirectionState.EAST:
                body.m_FollowOffset = new Vector3(-offset, 0 , 0);
                return new Vector3(transform.position.x - offset, player.position.y, player.position.z);
            
            case DirectionState.SOUTH:
                body.m_FollowOffset = new Vector3(0, 0, offset);
                return new Vector3(player.position.x, player.position.y, transform.position.z + offset);
            
            case DirectionState.WEST:
                body.m_FollowOffset = new Vector3(offset, 0, 0);
                return new Vector3(transform.position.x + offset, player.position.y, player.position.z);
        }

        return Vector3.zero;
    }

    private void ViewObstructed()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, player.position - transform.position, out hit, offset + 4))
        {
            if (hit.collider.gameObject.tag != "Player")
            {
                obstruction = hit.transform;
                obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            }
            else
            {
                if (obstruction != null)
                {
                    obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                }
            }
        }
    }
}
