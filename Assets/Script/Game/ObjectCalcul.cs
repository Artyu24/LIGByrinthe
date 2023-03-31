using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCalcul : MonoBehaviour
{
    private void Start()
    {
        WallManager.instance.setupObject += CheckObject;
        CheckObject();
    }

    private void CheckObject()
    {
        Vector2 playerVec = new Vector2(GameManager.instance.Player.position.x - Camera.main.transform.position.x, GameManager.instance.Player.position.z - Camera.main.transform.position.z);
        Vector2 objectVec = new Vector2(transform.position.x - GameManager.instance.Player.position.x, transform.position.z - GameManager.instance.Player.position.z);
        float angle = Vector2.SignedAngle(playerVec, objectVec);

        if (Mathf.Abs(angle) > 110)
        {
            gameObject.layer = 6;
            
            foreach (Transform obj in transform)
            {
                obj.gameObject.layer = 6;
            }
        }
        else
        {
            gameObject.layer = 0;

            foreach (Transform obj in transform)
            {
                obj.gameObject.layer = 0;
            }
        }
    }
}
