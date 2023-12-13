using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopperVein : OreVein
{
    public override Materials MaterialType
    {
        get
        {
            return Materials.Copper;
        }
    }
}
