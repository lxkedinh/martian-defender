using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class MouseController : MonoBehaviour, IPointerClickHandler
{
    public static MouseController Instance { get; private set; }

    private readonly Mouse mouse = Mouse.current;
    public SpriteRenderer spriteRenderer;
    public Tilemap tilemap;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

    }

    void FixedUpdate()
    {
        if (PlayerController.Instance.playMode == PlayMode.Normal)
        {
            HideCursorTile();
            return;
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);


        if (hit.collider == null || !hit.collider.name.Equals("Tilemap"))
        {
            return;
        }


        ShowCursorTile(mousePos);
    }

    public void ShowCursorTile(Vector2 mousePos)
    {
        Vector3Int cell = tilemap.WorldToCell(mousePos);

        transform.position = tilemap.GetCellCenterWorld(cell);
        spriteRenderer.enabled = true;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
    }

    public void HideCursorTile()
    {
        spriteRenderer.enabled = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MapController.Instance.PlaceObject(transform.position);
    }
}
