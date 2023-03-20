using System;
using UnityEngine;

namespace Environment.MoveGrid
{
    [Serializable]
    public class MovePoint
    {
        public int NeighborTopIndex;
        public int NeighborDownIndex;
        public int NeighborLeftIndex;
        public int NeighborRightIndex;
        public Vector3 LocalPosition;
        public bool IsOccupied;
        public MeshRenderer MeshRenderer;

        public MovePoint(MeshRenderer meshRenderer, Vector3 position)
        {
            MeshRenderer = meshRenderer;
            LocalPosition = 
            LocalPosition = position;
            NeighborDownIndex = -1;
            NeighborLeftIndex = -1;
            NeighborRightIndex = -1;
            NeighborTopIndex = -1;
        }
    }
}