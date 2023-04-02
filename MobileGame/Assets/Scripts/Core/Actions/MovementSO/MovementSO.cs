using UnityEngine;

namespace Actions
{
    [CreateAssetMenu(menuName = "Actions/MovementActionSO", fileName = "new MovementAction")]
    public class MovementSO : ScriptableObject
    {
        public AnimationCurve CurvePosition;
        public float MaxTime;
        private void OnEnable()
        {
            
        }
    }
}