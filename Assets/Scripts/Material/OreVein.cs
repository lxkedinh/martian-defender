using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OreVein : MonoBehaviour
{
    public abstract Materials MaterialType { get; }

    public void BreakOre()
    {
        InventoryController.Instance.AddMaterial(MaterialType, Random.Range(1, 3));
        Destroy(gameObject);
    }
}
