using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GFCompassArrow : MonoBehaviour
{
    void Start()
    {
        RectTransform rect = transform.GetComponent<RectTransform>();

        Sequence seq = DOTween.Sequence();
        Tween one = rect.DORotate(new Vector3(0, 0, -10), Random.Range(0.5f, 1.2f)).SetEase(Ease.InOutBack);
        Tween two = rect.DORotate(new Vector3(0, 0, 10), Random.Range(0.5f, 1.2f)).SetEase(Ease.InOutBack);
        Tween three = rect.DORotate(new Vector3(0, 0, -2), Random.Range(0.5f, 1.2f)).SetEase(Ease.InOutBack);
        Tween foor = rect.DORotate(new Vector3(0, 0, 2), Random.Range(0.5f, 1.2f)).SetEase(Ease.InOutBack);
        seq.Append(one).Append(two).Append(three).Append(foor).SetLoops(-1);
    }
}
