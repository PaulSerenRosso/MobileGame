using UnityEngine;

namespace Action
{
    [CreateAssetMenu(menuName = "Attack/AttackSO", fileName = "new AttackSO")]
    public class AttackActionSO : ScriptableObject
    {
        public HitSO[] HitsSO;
    }
}