using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    private RectTransform rect;

    private void Start()
    {
        GameObject playerBubble = Resources.Load<GameObject>("PlayerBubble");
        rect = Instantiate(playerBubble, UIManager.instance.CanvasUI).GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
    }

    public void Update()
    {
        rect.position = MapCameraManager.LvlCamera.WorldToScreenPoint(transform.position);
    }
}
