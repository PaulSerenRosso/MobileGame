using UnityEngine;

namespace Action
{
    [CreateAssetMenu(menuName = "Actions/AttackSO", fileName = "new AttackSO")]
    public class AttackActionSO : ScriptableObject
    {
        public HitSO[] HitsSO;
    }
}