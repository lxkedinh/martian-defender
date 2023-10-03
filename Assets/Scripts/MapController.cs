using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    public static MapController Instance { get; private set; }
    public Tilemap tilemap;
    public OverlayTile overlayTilePrefab;
    public GameObject overlayContainer;

    private Dictionary<Vector2Int, OverlayTile> map;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        BoundsInt bounds = tilemap.cellBounds;
        map = new Dictionary<Vector2Int, OverlayTile>();

        // looping through all tiles from top to bottom
        for (int z = bounds.max.z; z >= bounds.min.z; z--)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                for (int x = bounds.min.x; x < bounds.max.x; x++)
                {
                    Vector3Int tileLocation = new(x, y, z);
                    Vector2Int tileKey = new(x, y);
                    if (tilemap.HasTile(tileLocation) && !map.ContainsKey(tileKey))
                    {
                        OverlayTile overlayTile = Instantiate(overlayTilePrefab, overlayContainer.transform);
                        Vector3 gridLocation = tilemap.GetCellCenterWorld(tileLocation);
                        overlayTile.transform.position = new Vector3(gridLocation.x, gridLocation.y, gridLocation.z + 1);
                        overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tilemap.GetComponent<TilemapRenderer>().sortingOrder;
                        map.Add(tileKey, overlayTile);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
