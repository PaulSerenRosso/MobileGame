using UnityEngine;

namespace Actions
{
    [CreateAssetMenu(menuName = "Actions/HitSO", fileName = "new HitSO")]
    public class HitSO : ScriptableObject
    {
        public float Damage;
        public float CancelTime;
        public float TimeBeforeHit;
        public float RecoveryTime;
        public float ComboTime;
        public int HitMovePointsDistance;
        [Tooltip("Number of hype that increases for a hit")] 
        public float HypeAmount;
        public GameObject Particle;
    }
}