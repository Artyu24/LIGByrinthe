using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SetupObjectDelegate();
public class WallManager : MonoBehaviour
{
    public static WallManager instance;

    public List<WallObject> allWalls = new List<WallObject>();
    private List<WallObject> hiddenWalls = new List<WallObject>();

    public SetupObjectDelegate setupObject;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        //Remove all Null Object
        for (int i = 0; i < allWalls.Count;)
        {
            if (!allWalls[i])
            {
                allWalls.Remove(allWalls[i]);
                continue;
            }

            i++;
        }
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
        setupObject();

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
