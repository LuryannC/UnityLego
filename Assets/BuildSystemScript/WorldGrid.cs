using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldGrid
{
    public static Vector3 GridPositionFromWorldPosition(Vector3 worldPosition, float gridScale)
    {
        var x = MathF.Round(worldPosition.x / gridScale) * gridScale;
        var y = MathF.Round(worldPosition.y / gridScale) * gridScale;
        var z = MathF.Round(worldPosition.z / gridScale) * gridScale;
        return new Vector3(x,y,z);
    }
    
}
