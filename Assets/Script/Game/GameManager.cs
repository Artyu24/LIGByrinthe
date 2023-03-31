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
    private static SwitchLevelDelegate setupNextLevel = null;
    public static SwitchLevelDelegate SetupNextLevel => setupNextLevel;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        foreach (NewLevelSetup point in playerSpawnPoints)
        {
            playerPointQueue.Enqueue(point);
        }

        setupNextLevel = SetupPlayerAndCam;
    }
    private void Start()
    {
        resetAndSwitch += MapCameraManager.NextLvlCam;
        resetAndSwitch += SetupNextLevel;

        resetAndSwitch();
    }

    private void SetupPlayerAndCam()
    {
        NewLevelSetup nextSetup = playerPointQueue.Dequeue();
        player.position = nextSetup.spawnPoint.position;

        CameraMovement.SetupCam(nextSetup.camDirection);
    }


    [Serializable]
    struct NewLevelSetup
    {
        public Transform spawnPoint;
        public DirectionState camDirection;
    }
}
