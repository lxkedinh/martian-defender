using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronVein : OreVein
{
    public override Materials MaterialType
    {
        get
        {
            return Materials.Iron;
        }
    }
}
