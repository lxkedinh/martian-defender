using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
