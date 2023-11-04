using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBody : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject outlineIndicator;
    public void ShowOutline()
    {
        outlineIndicator.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void HideOutline()
    {
        outlineIndicator.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowOutline();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideOutline();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MapController.Instance.SelectTower(GetComponentInParent<Tower>());
    }
}
