using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    public static MapController Instance { get; private set; }
    public Tower towerPrefab;
    public HashSet<Tower> towersPlaced = new();


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
}
