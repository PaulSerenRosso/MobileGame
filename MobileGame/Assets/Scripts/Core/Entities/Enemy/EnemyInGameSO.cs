using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(menuName = "EnemyInGameSO", fileName = "new EnemyInGameSO")]
    public class EnemyInGameSO : ScriptableObject
    {
        [Header("Stats")]
        public float PercentageHealthStun;
        public float PercentageDamageReduction;
        public float TimeStunAvailable;
        public float TimeInvulnerable;
        public float AngleBlock;
        public float AngleStun;
        public float PercentageDamageReductionBoostChimist;
    }
}