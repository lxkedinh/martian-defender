using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Wall : MonoBehaviour
{
    public GameObject wallPrefab;
    public static int buildCost = 1;
    public static bool CanBuild
    {
        get
        {
            return InventoryController.Instance.inventory[Materials.Iron] >= buildCost;
        }
    }

    public void OnDeath()
    {
        Destroy(gameObject);
        NavMeshSurface surface = FindObjectOfType<NavMeshSurface>();
        if (surface != null)
        {
            surface.BuildNavMesh();
        }
    }
}
