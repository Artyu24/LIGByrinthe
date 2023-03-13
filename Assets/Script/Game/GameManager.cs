using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private List<Transform> playerSpawnPoints = new List<Transform>();
    private Queue<Transform> playerPointQueue = new Queue<Transform>();

    private static SwitchLevelDelegate tpPlayer = null;
    public static SwitchLevelDelegate TpPlayer => tpPlayer;

    private void Awake()
    {
        foreach (Transform point in playerSpawnPoints)
        {
            playerPointQueue.Enqueue(point);
        }

        tpPlayer = TeleportPlayerNextLevel;
    }

    private void TeleportPlayerNextLevel()
    {
        player.position = playerPointQueue.Dequeue().position;
    }
}
