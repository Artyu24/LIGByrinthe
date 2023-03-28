using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestDeMerde : MonoBehaviour
{
    public Text ntm;
    public Transform player;

    void Update()
    {
        ntm.text = "X: " + player.position.x + " Y: " + player.position.y + " Z: " + player.position.z;
    }
}
