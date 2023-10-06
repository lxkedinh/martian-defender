using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class MouseController : MonoBehaviour
{
    public Tower towerPrefab;
    private readonly Mouse mouse = Mouse.current;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        RaycastHit2D? cursorHit = GetObjectOnCursor();
        if (cursorHit.HasValue)
        {
            if (cursorHit.Value.collider.gameObject.TryGetComponent<OverlayTile>(out OverlayTile tile))
            {
                ShowCursorIndicator(tile);

                // spawn tower prefab on tile click
                if (mouse.leftButton.wasPressedThisFrame)
                {
                    PlaceTowerOnTile(tile);
                }
            }

            if (cursorHit.Value.collider.gameObject.TryGetComponent<Tower>(out Tower tower) && mouse.leftButton.wasPressedThisFrame)
            {
                foreach (Tower placedTower in MapController.Instance.towersPlaced)
                {
                    placedTower.Deselect();
                }
                tower.Select();
            }
        }
    }

    public void ShowCursorIndicator(OverlayTile tile)
    {
        transform.position = tile.transform.position;
        GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
    }

    private void PlaceTowerOnTile(OverlayTile tile)
    {
        foreach (Tower placedTower in MapController.Instance.towersPlaced)
        {
            placedTower.Deselect();
        }

        Tower tower = Instantiate(towerPrefab).GetComponent<Tower>();
        tower.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z);
        tower.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
        MapController.Instance.towersPlaced.Add(tower);
        tower.Select();
    }


    public RaycastHit2D? GetObjectOnCursor()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
