using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLevel : MonoBehaviour
{
    private bool done = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!done)
        {
            done = true;
            TransitionEffect.EnterEffect();
        }
    }
}