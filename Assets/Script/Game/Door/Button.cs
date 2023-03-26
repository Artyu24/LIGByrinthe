using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Door door;

    private GameObject interactionBubblePrefab;
    private GameObject bubbleTemp = null;

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
        
        //Anim Bouton
    }
}
