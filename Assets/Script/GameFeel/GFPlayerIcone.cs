using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GFPlayerIcone : MonoBehaviour
{
    private CanvasGroup gr;

    void Start()
    {
        gr = GetComponent<CanvasGroup>();

        Sequence seq = DOTween.Sequence();
        Tween one = gr.DOFade(0.2f, 0.4f);
        Tween two = gr.DOFade(1, 0.4f); ;
        seq.Append(one).Append(two).SetLoops(-1);
    }
}
