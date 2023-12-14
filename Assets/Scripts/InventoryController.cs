using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance { get; private set; }

    public Dictionary<Materials, int> inventory;
    public TMP_Text inventoryUI;

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
            { Materials.Iron, 0 },
            { Materials.Copper, 0}
        };
    }

    void Update()
    {
        inventoryUI.text =
            $"{inventory[Materials.Copper]} <sprite name=\"icon_copper_ingot\">" +
            "\n" +
            $"{inventory[Materials.Iron]} <sprite name=\"icon_iron_ingot\">";
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
