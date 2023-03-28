using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using static UnityEditor.PlayerSettings;

public class InteractionBubble : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private TextMeshProUGUI text;
    private Sequence seq;
    private Vector3 posToStay = Vector3.zero;

    public void SetPos(Vector3 pos)
    {
        rect.position = MapCameraManager.LvlCamera.WorldToScreenPoint(pos);
        posToStay = pos;

        seq = DOTween.Sequence();
        Tween one = rect.DOScale(new Vector3(1.4f, 1.4f), 0.5f).SetEase(Ease.InOutSine);
        Tween two = rect.DOScale(new Vector3(1, 1), 0.5f).SetEase(Ease.InOutSine);
        seq.Append(one).Append(two).SetLoops(5).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    private void Update()
    {
        if(posToStay != Vector3.zero)
            rect.position = MapCameraManager.LvlCamera.WorldToScreenPoint(posToStay);
    }

    private void OnDestroy()
    {
        seq.Kill();
    }
}
