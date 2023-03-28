using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    private RectTransform rectSprite;
    private RectTransform rectShadow;
    private Vector3 offset = Vector3.zero;

    private void Start()
    {
        GameObject playerBubble = Resources.Load<GameObject>("PlayerBubble");
        rectSprite = Instantiate(playerBubble, UIManager.instance.CanvasUI).GetComponent<RectTransform>();
        
        GameObject mapShadow = Resources.Load<GameObject>("ShadowMask");
        rectShadow = Instantiate(mapShadow, UIManager.instance.CanvasUI).GetComponent<RectTransform>();
        rectShadow.SetAsFirstSibling();
        rectShadow = rectShadow.GetChild(0).GetComponent<RectTransform>();
        offset = rectShadow.localPosition;
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
    }

    public void Update()
    {
        rectSprite.position = MapCameraManager.LvlCamera.WorldToScreenPoint(transform.position);
        rectShadow.position = MapCameraManager.LvlCamera.WorldToScreenPoint(transform.position) + offset;
    }
}
