using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    public Tower towerPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        RaycastHit2D? cursorHit = GetObjectOnCursor();
        if (cursorHit.HasValue)
        {
            // show cursor tile indicator for mouse hovering
            OverlayTile tile = cursorHit.Value.collider.gameObject.GetComponent<OverlayTile>();

            if (tile != null)
            {
                ShowCursorIndicator(tile);

                // spawn tower prefab on tile click
                Mouse mouse = Mouse.current;
                if (mouse.leftButton.wasPressedThisFrame)
                {
                    PlaceTowerOnTile(tile);
                }
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
        Tower tower = Instantiate(towerPrefab).GetComponent<Tower>();
        tower.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z);
        tower.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
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
