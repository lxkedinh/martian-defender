using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OverlayTile : MonoBehaviour
{
    void Update()
    {
        // Mouse mouse = Mouse.current;
        // if (mouse.leftButton.wasPressedThisFrame)
        // {
        //     HideTile();
        // }
    }

    public void ShowTile()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void HideTile()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
