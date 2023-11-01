using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class OverlayTile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Tower towerPrefab;

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowTile();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideTile();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Tower tower = Instantiate(towerPrefab);
        tower.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        MapController.Instance.towersPlaced.Add(tower);
        MapController.Instance.SelectTower(tower);
    }
}
