using UnityEngine;

namespace Action
{
    [CreateAssetMenu(menuName = "Attack/HitSO", fileName = "new HitSO")]
    public class HitSO : ScriptableObject
    {
        public float Damage;
        public float CancelTime;
        public float TimeBeforeHit;
        public float RecoveryTime;
        public float ComboTime;
        public float HitMovePointsDistance;
        public GameObject Particle;
    }
}