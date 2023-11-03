using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    public static MapController Instance { get; private set; }
    public Tilemap tilemap;
    public OverlayTile overlayTilePrefab;
    public Tower towerPrefab;
    public GameObject overlayContainer;
    public HashSet<Tower> towersPlaced = new();
    public Wall wallPrefab;

    private Dictionary<Vector2Int, OverlayTile> map;

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
        map = new Dictionary<Vector2Int, OverlayTile>();
        GenerateOverlayTiles();

    }

    private void GenerateOverlayTiles()
    {
        BoundsInt bounds = tilemap.cellBounds;
        // looping through all tiles from highest to lowest elevation
        for (int z = bounds.max.z; z >= bounds.min.z; z--)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                for (int x = bounds.min.x; x < bounds.max.x; x++)
                {
                    Vector3Int tileLocation = new(x, y, z);
                    Vector2Int tileKey = new(x, y);

                    // we only want to create overlay tiles to show cursor on the surface tiles
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

    public void SelectTower(Tower tower)
    {
        foreach (Tower placedTower in towersPlaced)
        {
            placedTower.Deselect();
        }
        tower.Select();
    }

    public void PlaceTowerOnTile(OverlayTile tile)
    {
        Tower tower = Instantiate(towerPrefab);
        tower.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z + 1);
        Instance.towersPlaced.Add(tower);
        Instance.SelectTower(tower);
    }


    public void PlaceObjectOnTile(OverlayTile tile, PlacementObjectType objectType)
    {
        switch (objectType)
        {
            case PlacementObjectType.Tower:
                PlaceTowerOnTile(tile);
                break;
            case PlacementObjectType.Wall:
                // Handle placing other object types here
                Wall wall = Instantiate(wallPrefab);
                wall.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z + 1);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
