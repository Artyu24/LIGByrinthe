using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Door door;

    private void OnTriggerEnter(Collider other)
    {
        door.ActionDoor();
        //Anim Bouton
    }
}
