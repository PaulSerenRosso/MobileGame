using UnityEngine;

namespace Actions
{
    [CreateAssetMenu(menuName = "Actions/AttackSO", fileName = "new AttackSO")]
    public class AttackActionSO : ScriptableObject
    {
        public HitSO[] HitsSO;
  
    }
}