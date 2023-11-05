using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    public static MapController Instance { get; private set; }
    public Tower towerPrefab;
    public HashSet<Tower> towersPlaced = new();
    public Wall wallPrefab;
    public HashSet<Wall> wallsPlaced = new();

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

    public void PlaceTower(Vector3 pos)
    {
        Tower tower = Instantiate(towerPrefab);
        tower.transform.position = new Vector3(pos.x, pos.y, pos.z + 1);
        Instance.towersPlaced.Add(tower);
        Instance.SelectTower(tower);
    }

    public void PlaceWall(Vector3 pos)
    {
        Wall wall = Instantiate(wallPrefab);
        wall.transform.position = new Vector3(pos.x, pos.y, pos.z + 1);
        Instance.wallsPlaced.Add(wall);
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
}

public enum ObjectPlacementType
{
    Tower,
    Wall
}

