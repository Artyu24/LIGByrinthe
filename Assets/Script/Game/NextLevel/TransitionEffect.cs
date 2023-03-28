using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TransitionEffect : MonoBehaviour
{
    private static int effectTime = 2;
    private static RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public static void EnterEffect()
    {
        rect.localPosition = new Vector3(2500, 0, 0);
        rect.DOLocalMoveX(0, effectTime).OnComplete(() =>
        {
            GameManager.ResetAndSwitch();
            ExitEffect();
        });
    }

    private static void ExitEffect()
    {
        rect.DOLocalMoveX(-2500, effectTime);
    }
}
