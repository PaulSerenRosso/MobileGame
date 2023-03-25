using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(menuName = "EnemySO", fileName = "new EnemySO")]
    public class EnemySO : ScriptableObject
    {
        public string Name;
        public float Health;
        public float PercentageHealth;
        public float TimeStunAvailable;
    }
}