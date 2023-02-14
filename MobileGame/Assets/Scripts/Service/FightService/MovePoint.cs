using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MovePoint
{
    public List<MovePoint> Neighbors;
    public Vector3 Position;

    private MeshRenderer _meshRenderer;
    private bool _isOccupied;

    public MovePoint(MeshRenderer meshRenderer, Vector3 position)
    {
        _meshRenderer = meshRenderer;
        Position = position;
        Neighbors = new List<MovePoint>();
    }
}