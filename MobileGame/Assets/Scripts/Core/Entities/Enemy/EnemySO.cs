using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(menuName = "EnemySO", fileName = "new EnemySO")]
    public class EnemySO : ScriptableObject
    {
        public string Name;
        
        [Header("Stats")]
        public float PercentageHealthStun;
        public float PercentageDamageReduction;
        public float TimeStunAvailable;
        public float TimeInvulnerable;
        public float AngleBlock;
        public int Rounds;
    }
}