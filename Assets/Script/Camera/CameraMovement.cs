using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public delegate void SetupCamDelegate(DirectionState dirWanted);

public class CameraMovement : MonoBehaviour
{
    [Header("Player Camera")]
    [SerializeField] private Transform playerCameraPoint;
    [SerializeField] private Transform mapCamera;
    [SerializeField] private float offset = 5f;

    [Header("Compass")]
    [SerializeField] private Transform compass;
    private List<RectTransform> compassLetterList = new List<RectTransform>();

    [Header("Divers")]
    [SerializeField] private float animationTime = 1f;
    private DirectionState[] allDir = new DirectionState[4] {DirectionState.NORTH, DirectionState.EAST, DirectionState.SOUTH, DirectionState.WEST};
    private int idDir = 0;
    private bool isInMovement = false;

    [Header("Cinemachine")]
    private CinemachineVirtualCamera brain;
    private CinemachineTransposer body;

    [Header("Delegate")]
    private static SetupCamDelegate setupCam = null;
    public static SetupCamDelegate SetupCam => setupCam;

    public delegate DirectionState DirectionDelegate();
    private static DirectionDelegate getDirection = null;
    public static DirectionDelegate GetDirection => getDirection;


    private void Awake()
    {
        brain = GetComponent<CinemachineVirtualCamera>();
        body = brain.GetCinemachineComponent<CinemachineTransposer>();

        //Set Delegate
        setupCam = SetPlayerCam;
        getDirection = GetActualDirection;

        foreach (Transform child in compass)
        {
            compassLetterList.Add(child.GetComponent<RectTransform>());
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && !isInMovement)
        {
            //Debug.Log("Left");
            WallManager.instance.ResetWall();

            isInMovement = true;
            brain.Follow = null;
            transform.DOMove(SetupNextPosition(1), animationTime).OnComplete(() =>
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
            transform.DOMove(SetupNextPosition(-1), animationTime).OnComplete(() =>
            {
                WallManager.instance.DesacWall(allDir[idDir], playerCameraPoint.position);
                brain.Follow = playerCameraPoint;
                isInMovement = false;
            });
        }
    }

    private void SetPlayerCam(DirectionState dirWanted)
    {
        brain.Follow = null;

        while (dirWanted != allDir[idDir])
        {
            transform.position = SetupNextPosition(1, true);
        }

        brain.Follow = playerCameraPoint;

        WallManager.instance.DesacWall(dirWanted, playerCameraPoint.position);
    }

    private Vector3 SetupNextPosition(int modifier, bool instant = false)
    {
        RotateCompass(modifier, instant);

        RotateMapCam(modifier, instant);

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

    private void RotateCompass(int modifier, bool instant = false)
    {
        foreach (RectTransform letterRect in compassLetterList)
        {
            if(!instant)
                letterRect.DORotate(new Vector3(0, 0, letterRect.eulerAngles.z + 90 * modifier), animationTime);
            else
                letterRect.eulerAngles = new Vector3(0, 0, letterRect.eulerAngles.z + 90 * modifier);
        }
    }

    private void RotateMapCam(int modifier, bool instant = false)
    {
        if (!instant)
            mapCamera.DORotate(new Vector3(90, mapCamera.eulerAngles.y + 90 * modifier), animationTime);
        else
            mapCamera.eulerAngles = new Vector3(90, mapCamera.eulerAngles.y + 90 * modifier);
    }

    public DirectionState GetActualDirection()
    {
        return allDir[idDir];
    }
}
