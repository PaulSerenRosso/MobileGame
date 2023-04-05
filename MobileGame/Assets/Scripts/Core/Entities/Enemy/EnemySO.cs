using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    [CreateAssetMenu(menuName = "EnemySO", fileName = "new EnemySO")]
    public class EnemySO : ScriptableObject
    {
        public string Name;
        
        [Header("Stats")]
        public float Health;
        public float PercentageHealthStun;
        public float PercentageDamageReduction;
        public float TimeStunAvailable;
        public float TimeInvulnerable;
        public float AngleBlock;
    }
}