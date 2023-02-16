using System;
using UnityEngine;

namespace Service.Inputs
{
    [CreateAssetMenu(menuName = "Inputs/Swipe", fileName = "new Swipe")]
    public class SwipeSO : ScriptableObject
    {
        public float Time;
        public Vector2 Direction;
        public float MinDistancePercentage;
        public float DirectionTolerance;

        private void OnValidate()
        {
            Direction = Direction.normalized;
        }
    }
}