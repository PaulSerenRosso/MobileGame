using UnityEngine;

namespace Action
{
    [CreateAssetMenu(menuName = "Attack/AttackSO", fileName = "new AttackSO")]
    public class AttackSO : ScriptableObject
    {
        public HitSO[] HitsSO;
    }
}