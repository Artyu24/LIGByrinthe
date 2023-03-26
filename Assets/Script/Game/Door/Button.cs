using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Door door;

    private GameObject interactionBubblePrefab;
    private GameObject bubbleTemp = null;

    [SerializeField] private Transform buttonPlate;

    private void Awake()
    {
        interactionBubblePrefab = Resources.Load<GameObject>("InteractionPoint");
    }

    private void OnTriggerEnter(Collider other)
    {
        door.ActionDoor();

        if(bubbleTemp != null)
            Destroy(bubbleTemp);

        bubbleTemp = Instantiate(interactionBubblePrefab, UIManager.instance.CanvasUI);
        bubbleTemp.GetComponent<InteractionBubble>().SetPos(door.transform.position);

        buttonPlate.DOMoveY(-0.12f, 0.3f);
    }

    private void OnTriggerExit(Collider other)
    {
        buttonPlate.DOMoveY(0.05f, 0.3f);
    }
}
