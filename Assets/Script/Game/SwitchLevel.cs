using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SwitchLevelDelegate();

public class SwitchLevel : MonoBehaviour
{
    private static SwitchLevelDelegate resetAndSwitch = null;
    public static SwitchLevelDelegate ResetAndSwitch => resetAndSwitch;

    private void Start()
    {
        resetAndSwitch += CameraMovement.ResetPlayerCam;
        resetAndSwitch += MapCameraManager.NextLvlCam;
        resetAndSwitch += GameManager.TpPlayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        TransitionEffect.EnterEffect();
    }
}