using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private List<NewLevelSetup> playerSpawnPoints = new List<NewLevelSetup>();
    private Queue<NewLevelSetup> playerPointQueue = new Queue<NewLevelSetup>();

    private static SwitchLevelDelegate setupNextLevel = null;
    public static SwitchLevelDelegate SetupNextLevel => setupNextLevel;

    private void Awake()
    {
        foreach (NewLevelSetup point in playerSpawnPoints)
        {
            playerPointQueue.Enqueue(point);
        }

        setupNextLevel = SetupPlayerAndCam;
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
