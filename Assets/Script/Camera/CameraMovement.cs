using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform compass;
    [SerializeField] private float animationTime = 1f;
    [SerializeField] private float offset = 5f;

    private List<RectTransform> compassLetterList = new List<RectTransform>();

    private DirectionState[] allDir = new DirectionState[4] {DirectionState.NORTH, DirectionState.EAST, DirectionState.SOUTH, DirectionState.WEST};
    private int idDir = 0;
    private bool isInMovement = false;

    private CinemachineVirtualCamera brain;
    private CinemachineTransposer body;

    private void Awake()
    {
        brain = GetComponent<CinemachineVirtualCamera>();
        body = brain.GetCinemachineComponent<CinemachineTransposer>();

        body.m_FollowOffset = new Vector3(0, 0, -offset);
        transform.position =  new Vector3(player.position.x, player.position.y, transform.position.z - offset);
        
        brain.Follow = player;

        foreach (Transform child in compass)
        {
            compassLetterList.Add(child.GetComponent<RectTransform>());
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isInMovement)
        {
            //Debug.Log("Left");
            isInMovement = true;
            brain.Follow = null;
            transform.DOMove(GetNextPosition(1), animationTime).OnComplete(() =>
            {
                WallManager.instance.DesacWall(allDir[idDir], player.position);
                brain.Follow = player;
                isInMovement = false;
            });
        }

        if (Input.GetKeyDown(KeyCode.D) && !isInMovement)
        {
            //Debug.Log("Droite");
            isInMovement = true;
            brain.Follow = null;
            transform.DOMove(GetNextPosition(-1), animationTime).OnComplete(() =>
            {
                WallManager.instance.DesacWall(allDir[idDir], player.position);
                brain.Follow = player;
                isInMovement = false;
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

        foreach (RectTransform letterRect in compassLetterList)
        {
            letterRect.DORotate(new Vector3(0, 0, letterRect.eulerAngles.z + 90 * modifier), animationTime);
        }

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
}
