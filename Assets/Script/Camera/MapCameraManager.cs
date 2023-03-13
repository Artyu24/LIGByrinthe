using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraManager : MonoBehaviour
{
    [SerializeField] private Transform lvlCamera;
    [SerializeField] private List<Transform> cameraLvlPoint = new List<Transform>();
    private Queue<Transform> lvlPointQueue = new Queue<Transform>();

    private static SwitchLevelDelegate nextLvlCam = null;
    public static SwitchLevelDelegate NextLvlCam => nextLvlCam;

    private void Awake()
    {
        foreach (Transform point in cameraLvlPoint)
        {
            lvlPointQueue.Enqueue(point);
        }

        nextLvlCam = SwitchCam;
    }

    private void SwitchCam()
    {
        lvlCamera.position = lvlPointQueue.Dequeue().position;
        lvlCamera.eulerAngles = new Vector3(90, 0, 0);
    }
}
