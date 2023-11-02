using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class MouseController : MonoBehaviour
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

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider == null || hit.collider.name != "Tilemap" || hit.collider.name != "Cursor")
        {
            Debug.Log(hit.collider.name == "Tilemap");
            spriteRenderer.enabled = false;
            return;
        }

        ShowCursorTile(mousePos);

        if (mouse.leftButton.wasPressedThisFrame)
        {
            MapController.Instance.PlaceTower(transform.position);
        }
    }

    public void ShowCursorTile(Vector2 mousePos)
    {
        Vector3Int cell = tilemap.WorldToCell(mousePos);
        if (!tilemap.HasTile(cell))
        {
            spriteRenderer.enabled = false;
            return;
        }

        transform.position = tilemap.GetCellCenterWorld(cell);
        spriteRenderer.enabled = true;
    }
}
