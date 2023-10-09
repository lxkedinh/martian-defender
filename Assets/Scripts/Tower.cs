using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Attack))]
[RequireComponent(typeof(Health))]
public class Tower : MonoBehaviour
{
    public int rangeRadius;
    private bool isSelected;
    public GameObject rangeIndicator;
    public GameObject outlineIndicator;

    // Start is called before the first frame update
    void Start()
    {
        rangeRadius = 4;
        float scaleFactor = (2 * rangeRadius) + 1f;
        rangeIndicator.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }

    void OnMouseEnter()
    {
        ShowOutline();
    }

    void OnMouseExit()
    {
        if (!isSelected)
        {
            HideOutline();
        }
    }

    public void Select()
    {
        isSelected = true;
        rangeIndicator.GetComponent<SpriteRenderer>().enabled = true;
        ShowOutline();
    }

    public void ShowOutline()
    {
        outlineIndicator.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void HideOutline()
    {
        outlineIndicator.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void Deselect()
    {
        isSelected = false;
        rangeIndicator.GetComponent<SpriteRenderer>().enabled = false;
        HideOutline();
    }
}
