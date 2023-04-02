using UnityEngine;

namespace Service.Inputs
{
    [CreateAssetMenu(menuName = "Inputs/Swipe", fileName = "new Swipe")]
    public class SwipeSO : ScriptableObject
    {
        public float Time;
        public Vector2 DirectionV2;
        public float MinDistancePercentage;
        public float DirectionTolerance;
        
        [HideInInspector] public Vector3 DirectionV3;

        private void OnValidate()
        {
            DirectionV2 = DirectionV2.normalized;
            DirectionV3 = new Vector3(DirectionV2.x, 0, DirectionV2.y);
        }
    }
}