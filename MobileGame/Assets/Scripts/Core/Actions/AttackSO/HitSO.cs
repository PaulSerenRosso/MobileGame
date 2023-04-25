using UnityEngine;

namespace Actions
{
    [CreateAssetMenu(menuName = "Actions/HitSO", fileName = "new HitSO")]
    public class HitSO : ScriptableObject
    {
        public float CancelTime;
        public float TimeBeforeHit;
        public float RecoveryTime;
        public float ComboTime;
        public int HitMovePointsDistance;
        public float HypeAmount;
        public GameObject Particle;
        public string NameAnimationTrigger;
        
    }
}