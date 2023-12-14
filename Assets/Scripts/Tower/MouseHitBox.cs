using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHitBox : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Outline outline;

    public void OnPointerEnter(PointerEventData eventData)
    {
        outline.ShowOutline();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!GetComponentInParent<Tower>().isSelected)
        {
            outline.HideOutline();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked");
        MapController.Instance.SelectTower(GetComponentInParent<Tower>());
    }
}
