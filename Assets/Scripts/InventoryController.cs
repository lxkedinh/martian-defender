using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance { get; private set; }

    public Dictionary<Materials, int> inventory;

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

    void Start()
    {
        inventory = new()
        {
            { Materials.Iron, 5 },
            { Materials.Copper, 15}
        };
    }

    public void AddMaterial(Materials m, int quantity)
    {
        inventory[m] += quantity;
    }

    public void RemoveMaterial(Materials m, int quantity)
    {
        int current = inventory[m];
        inventory[m] = Mathf.Clamp(current - quantity, 0, current);
    }
}

public enum Materials
{
    Iron,
    Copper
}
