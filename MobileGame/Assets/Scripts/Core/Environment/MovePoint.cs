using System;
using System.Collections.Generic;
using UnityEngine;

namespace Environnement.MoveGrid
{
[Serializable]
public class MovePoint
{
    public List<int> Neighbors;
    public Vector3 Position;
    public bool IsOccupied;

    public MeshRenderer MeshRenderer;

    public MovePoint(MeshRenderer meshRenderer, Vector3 position)
    {
        MeshRenderer = meshRenderer;
        Position = position;
        Neighbors = new List<int>();
    }
}
}
