using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class MovePoint
{
    public List<int> Neighbors;
    public Vector3 Position;

    public MeshRenderer MeshRenderer;
    private bool _isOccupied;

    public MovePoint(MeshRenderer meshRenderer, Vector3 position)
    {
        MeshRenderer = meshRenderer;
        Position = position;
        Neighbors = new List<int>();
    }
}