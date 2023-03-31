using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void SwitchLevelDelegate();
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Transform player;
    public Transform Player => player;
    [SerializeField] private List<NewLevelSetup> playerSpawnPoints = new List<NewLevelSetup>();
    private Queue<NewLevelSetup> playerPointQueue = new Queue<NewLevelSetup>();

    [Header("Delegate")]
    private static SwitchLevelDelegate resetAndSwitch = null;
    public static SwitchLevelDelegate ResetAndSwitch => resetAndSwitch;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        foreach (NewLevelSetup point in playerSpawnPoints)
        {
            playerPointQueue.Enqueue(point);
        }

        resetAndSwitch += SetupPlayerAndCam;
    }
    private void Start()
    {
        resetAndSwitch();
    }

    private void SetupPlayerAndCam()
    {
        NewLevelSetup nextSetup = playerPointQueue.Dequeue();
        player.position = nextSetup.spawnPoint.position;

        MapCameraManager.NextLvlCam(nextSetup.camDirection);
        CameraMovement.SetupCam(nextSetup.camDirection);

        if (playerPointQueue.Count == 0)
        {
            resetAndSwitch = null;
        }
    }


    [Serializable]
    struct NewLevelSetup
    {
        public Transform spawnPoint;
        public DirectionState camDirection;
    }
}
