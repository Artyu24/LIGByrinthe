using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        TransitionEffect.EnterEffect();
    }
}