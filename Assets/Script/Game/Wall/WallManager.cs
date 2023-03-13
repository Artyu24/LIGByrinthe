using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public static WallManager instance;

    [SerializeField] private List<WallObject> allWalls;
    private List<WallObject> hiddenWalls = new List<WallObject>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void ResetWall()
    {
        foreach (WallObject hiddenWall in hiddenWalls)
        {
            hiddenWall.gameObject.layer = 0;
        }
        hiddenWalls.Clear();
    }

    public void DesacWall(DirectionState state, Vector3 playerPos)
    {
        foreach (WallObject wall in allWalls)
        {
            if (wall.isWallFrontOfPlayer(state, playerPos))
            {
                wall.gameObject.layer = 6;
                hiddenWalls.Add(wall);
            }
            else
            {
                wall.gameObject.layer = 0;
            }
        }
    }
}
