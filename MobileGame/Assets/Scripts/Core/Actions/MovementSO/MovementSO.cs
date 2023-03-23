using UnityEngine;

namespace Actions
{
    [CreateAssetMenu(menuName = "Actions/MovementActionSO", fileName = "new MovementAction")]
    public class MovementSO : ScriptableObject
    {
        public float MaxTime;
        public AnimationCurve CurvePosition;

        private void OnEnable()
        {
            
        }
    }
}