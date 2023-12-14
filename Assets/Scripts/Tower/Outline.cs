using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;

    public void ChangeOutline(int index)
    {
        spriteRenderer.sprite = sprites[index];
    }

    public void ShowOutline()
    {
        spriteRenderer.enabled = true;
    }

    public void HideOutline()
    {
        spriteRenderer.enabled = false;
    }
}
