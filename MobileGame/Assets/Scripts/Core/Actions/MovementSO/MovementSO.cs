using UnityEngine;

namespace Action
{
    [CreateAssetMenu(menuName = "PlayerMovementSO", fileName = "new PlayerMovement")]
    public class MovementSO : ScriptableObject
    {
        public float MaxTime;
        public AnimationCurve CurvePosition;
    }
}