using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Components;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class MapController : MonoBehaviour, IPointerClickHandler
{
    public static MapController Instance { get; private set; }
    public Tower towerPrefab;
    public HashSet<Tower> towersPlaced = new();
    public Wall wallPrefab;
    public HashSet<Wall> wallsPlaced = new();

    public NavMeshSurface Surface2D;

    public ObjectPlacementType objectPlacementType = ObjectPlacementType.Tower;

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

    public void SelectTower(Tower tower)
    {
        foreach (Tower placedTower in towersPlaced)
        {
            placedTower.Deselect();
        }
        tower.Select();
    }

    public void DeselectStructures()
    {
        foreach (Tower placedTower in towersPlaced)
        {
            placedTower.Deselect();
        }
    }

    public void PlaceTower(Vector3 pos)
    {
        if (!Tower.CanBuild) return;

        InventoryController.Instance.RemoveMaterial(Materials.Copper, Tower.buildCost);
        Tower tower = Instantiate(towerPrefab);
        tower.transform.position = new Vector3(pos.x, pos.y, pos.z);
        Instance.towersPlaced.Add(tower);
        Instance.SelectTower(tower);

        Surface2D.BuildNavMesh();
    }

    public void DeleteTower(Tower tower)
    {
        InventoryController.Instance.AddMaterial(Materials.Copper, Tower.buildCost);
        Destroy(tower.gameObject);
    }

    public void PlaceWall(Vector3 pos)
    {
        if (!Wall.CanBuild) return;

        InventoryController.Instance.RemoveMaterial(Materials.Iron, Wall.buildCost);
        Wall wall = Instantiate(wallPrefab);
        wall.transform.position = new Vector3(pos.x, pos.y, pos.z);
        Instance.wallsPlaced.Add(wall);

        Surface2D.BuildNavMesh();
    }

    public void PlaceObject(Vector3 pos)
    {
        if (objectPlacementType == ObjectPlacementType.Tower)
        {
            PlaceTower(pos);
        }
        else
        {
            PlaceWall(pos);
        }
    }

    public void ChangeObjectPlacementType()
    {
        if (objectPlacementType == ObjectPlacementType.Tower)
        {
            objectPlacementType = ObjectPlacementType.Wall;
        }
        else
        {
            objectPlacementType = ObjectPlacementType.Tower;
        }
    }

    public void ResetMap()
    {
        towersPlaced.Clear();
        foreach (var tower in GameObject.FindGameObjectsWithTag("Tower"))
        {
            Destroy(tower);
        }

        wallsPlaced.Clear();
        foreach (var wall in GameObject.FindGameObjectsWithTag("Wall"))
        {
            Destroy(wall);
        }

        Ship.Instance.GetComponent<SpriteRenderer>().enabled = true;
        Ship.Instance.GetComponent<Health>().ResetHealth();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DeselectStructures();
    }
}

public enum ObjectPlacementType
{
    Tower,
    Wall
}

