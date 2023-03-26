using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 firstPos;
    public Vector3 firstRot;

    public Vector3 secondPos;
    public Vector3 secondRot;

    private bool isOpen = false;

    public void ActionDoor()
    {
        if (isOpen)
        {
            transform.DOMove(firstPos, 0.5f);
            transform.DORotate(firstRot, 0.5f);
        }
        else
        {
            transform.DOMove(secondPos, 0.5f);
            transform.DORotate(secondRot, 0.5f);
        }

        isOpen = !isOpen;
    }
}
