using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class MouseController : MonoBehaviour
{
    public static MouseController Instance { get; private set; }

    private readonly Mouse mouse = Mouse.current;
    private SpriteRenderer spriteRenderer;

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

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        RaycastHit2D? cursorHit = GetObjectOnCursor();
        if (cursorHit.HasValue)
        {
            if (cursorHit.Value.collider.gameObject.TryGetComponent(out OverlayTile tile))
            {
                ShowCursorIndicator(tile);

                // spawn tower prefab on tile click
                if (mouse.leftButton.wasPressedThisFrame)
                {
                    MapController.Instance.PlaceTowerOnTile(tile);
                }
            }

            else if (cursorHit.Value.collider.gameObject.TryGetComponent(out Tower tower))
            {
                spriteRenderer.enabled = false;
                // tower.ShowOutline();

                if (mouse.leftButton.wasPressedThisFrame)
                {
                    MapController.Instance.SelectTower(tower);
                }
            }
        }
    }

    public void ShowCursorIndicator(OverlayTile tile)
    {
        transform.position = tile.transform.position;
        spriteRenderer.enabled = true;
        spriteRenderer.sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
    }



    public RaycastHit2D? GetObjectOnCursor()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
        Vector2 mousePos2d = new(mousePos.x, mousePos.y);
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2d, Vector2.zero);

        // return collider hit first highest in elevation sorted by z axis
        if (hits.Length > 0)
        {
            return hits.OrderByDescending(i => i.collider.transform.position.z).First();
        }
        return null;
    }
}
