using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public static WallManager instance;

    [SerializeField] private List<WallObject> wallObjects;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void DesacWall(DirectionState state, Vector3 playerPos)
    {
        foreach (WallObject wall in wallObjects)
        {
            if (wall.isWallFrontOfPlayer(state, playerPos))
            {
                wall.gameObject.SetActive(false);
            }
            else
            {
                wall.gameObject.SetActive(true);
            }
        }
    }
}
