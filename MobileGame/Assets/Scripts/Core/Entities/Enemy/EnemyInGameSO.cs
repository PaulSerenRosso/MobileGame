using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(menuName = "EnemyInGameSO", fileName = "new EnemyInGameSO")]
    public class EnemyInGameSO : ScriptableObject
    {
        [Header("Stats")]
        public float TimeInvulnerable;
        public float AngleBlock;
        public float AngleStun;
        public float PercentageDamageReductionBoostChimist;
    }
}