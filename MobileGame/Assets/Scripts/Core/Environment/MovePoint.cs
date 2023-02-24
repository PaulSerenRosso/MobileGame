using System;
using System.Collections.Generic;
using UnityEngine;

namespace Environnement.MoveGrid
{
[Serializable]
public class MovePoint
{
    
    public int neighborTopIndex;
    public int neighborDownIndex;
    public int neighborLeftIndex;
    public int neighborRightIndex;
    public Vector3 Position;
    public bool IsOccupied;

    public MeshRenderer MeshRenderer;

    public MovePoint(MeshRenderer meshRenderer, Vector3 position)
    {
        MeshRenderer = meshRenderer;
        Position = position;
        neighborDownIndex = -1;
        neighborLeftIndex = -1;
        neighborRightIndex = -1;
        neighborTopIndex = -1;
    }
}
}
