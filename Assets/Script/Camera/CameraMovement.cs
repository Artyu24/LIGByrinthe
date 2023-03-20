using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Player Camera")]
    [SerializeField] private Transform playerCameraPoint;
<<<<<<< HEAD
    [SerializeField] private Transform mapCamera;
=======
>>>>>>> parent of 7893da1 (Merge branch 'Maxens')
    [SerializeField] private float offset = 5f;

    [Header("Compass")]
    [SerializeField] private Transform compass;
<<<<<<< HEAD
    private List<RectTransform> compassLetterList = new List<RectTransform>();

    [Header("Divers")]
    [SerializeField] private float animationTime = 1f;
=======

    [Header("Divers")]
    [SerializeField] private float animationTime = 1f;

    private List<RectTransform> compassLetterList = new List<RectTransform>();

>>>>>>> parent of 7893da1 (Merge branch 'Maxens')
    private DirectionState[] allDir = new DirectionState[4] {DirectionState.NORTH, DirectionState.EAST, DirectionState.SOUTH, DirectionState.WEST};
    private int idDir = 0;
    private bool isInMovement = false;

    [Header("Cinemachine")]
    private CinemachineVirtualCamera brain;
    private CinemachineTransposer body;

    [Header("Delegate")]
    private static SwitchLevelDelegate resetPlayerCam = null;
    public static SwitchLevelDelegate ResetPlayerCam => resetPlayerCam;

    public delegate DirectionState DirectionDelegate();
    private static DirectionDelegate getDirection = null;
    public static DirectionDelegate GetDirection => getDirection;


    private void Awake()
    {
        brain = GetComponent<CinemachineVirtualCamera>();
        body = brain.GetCinemachineComponent<CinemachineTransposer>();

<<<<<<< HEAD
        SetPlayerCamNorth();
        resetPlayerCam = SetPlayerCamNorth;
        getDirection = GetActualDirection;
=======
        body.m_FollowOffset = new Vector3(0, 0, -offset);
        transform.position =  new Vector3(playerCameraPoint.position.x, playerCameraPoint.position.y, transform.position.z - offset);
        
        brain.Follow = playerCameraPoint;
>>>>>>> parent of 7893da1 (Merge branch 'Maxens')

        foreach (Transform child in compass)
        {
            compassLetterList.Add(child.GetComponent<RectTransform>());
        }
    }

    private void Start()
    {
        WallManager.instance.DesacWall(allDir[idDir], playerCameraPoint.position);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && !isInMovement)
        {
            //Debug.Log("Left");
            WallManager.instance.ResetWall();

            isInMovement = true;
            brain.Follow = null;
            transform.DOMove(GetNextPosition(1), animationTime).OnComplete(() =>
            {
                WallManager.instance.DesacWall(allDir[idDir], playerCameraPoint.position);
                brain.Follow = playerCameraPoint;
                isInMovement = false;
            });
        }

        if (Input.GetKeyDown(KeyCode.M) && !isInMovement)
        {
            //Debug.Log("Droite");
            WallManager.instance.ResetWall();

            isInMovement = true;
            brain.Follow = null;
            transform.DOMove(GetNextPosition(-1), animationTime).OnComplete(() =>
            {
                WallManager.instance.DesacWall(allDir[idDir], playerCameraPoint.position);
                brain.Follow = playerCameraPoint;
                isInMovement = false;
            });
        }
    }

    private void SetPlayerCamNorth()
    {
        body.m_FollowOffset = new Vector3(0, 0, -offset);
        transform.position = new Vector3(playerCameraPoint.position.x, playerCameraPoint.position.y, transform.position.z - offset);

        brain.Follow = playerCameraPoint;
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
<<<<<<< HEAD
        mapCamera.DORotate(new Vector3(90, mapCamera.eulerAngles.y + 90 * modifier), animationTime);
=======
>>>>>>> parent of 7893da1 (Merge branch 'Maxens')

        return allDir[idDir];
    }

    private Vector3 GetNextPosition(int modifier)
    {
        switch (GetNextDirection(modifier))
        {
            case DirectionState.NORTH:
                body.m_FollowOffset = new Vector3(0, 0, -offset);
                return new Vector3(playerCameraPoint.position.x, playerCameraPoint.position.y, transform.position.z - offset);
            
            case DirectionState.EAST:
                body.m_FollowOffset = new Vector3(-offset, 0 , 0);
                return new Vector3(transform.position.x - offset, playerCameraPoint.position.y, playerCameraPoint.position.z);
            
            case DirectionState.SOUTH:
                body.m_FollowOffset = new Vector3(0, 0, offset);
                return new Vector3(playerCameraPoint.position.x, playerCameraPoint.position.y, transform.position.z + offset);
            
            case DirectionState.WEST:
                body.m_FollowOffset = new Vector3(offset, 0, 0);
                return new Vector3(transform.position.x + offset, playerCameraPoint.position.y, playerCameraPoint.position.z);
        }

        return Vector3.zero;
    }

    public DirectionState GetActualDirection()
    {
        return allDir[idDir];
    }
}
