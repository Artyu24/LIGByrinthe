using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraManager : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform lvlCamera;
    [SerializeField] private List<Transform> cameraLvlPoint = new List<Transform>();
    private Queue<Transform> lvlPointQueue = new Queue<Transform>();
    private Vector2 velocity;

    private static Camera upCamera;
    public static Camera LvlCamera => upCamera;

    private static SetupCamDelegate nextLvlCam = null;
    public static SetupCamDelegate NextLvlCam => nextLvlCam;

    private void Awake()
    {
        upCamera = lvlCamera.GetComponent<Camera>();

        foreach (Transform point in cameraLvlPoint)
        {
            lvlPointQueue.Enqueue(point);
        }

        nextLvlCam = SwitchCam;
    }

    private void Update()
    {
        if (Vector2.Distance(new Vector2(GameManager.instance.Player.position.x, GameManager.instance.Player.position.z), new Vector2(lvlCamera.position.x, lvlCamera.position.z)) > upCamera.orthographicSize - 7)
        {

            if (Vector2.Distance(new Vector2(GameManager.instance.Player.position.x, 0), new Vector2(lvlCamera.position.x,0)) > upCamera.orthographicSize - 7)
            {
                lvlCamera.position = Vector3.MoveTowards(lvlCamera.position, new Vector3(GameManager.instance.Player.position.x, lvlCamera.position.y, lvlCamera.position.z), Time.deltaTime * speed);
            }

            if (Vector2.Distance(new Vector2(GameManager.instance.Player.position.z, 0), new Vector2(lvlCamera.position.z, 0)) > upCamera.orthographicSize - 7)
            {
                lvlCamera.position = Vector3.MoveTowards(lvlCamera.position, new Vector3(lvlCamera.position.x, lvlCamera.position.y, GameManager.instance.Player.position.z), Time.deltaTime * speed);
            }
        }
    }

    private void SwitchCam(DirectionState dir)
    {
        lvlCamera.position = lvlPointQueue.Dequeue().position;
    }
}
