using UnityEngine;

namespace Service.Hype
{
    [CreateAssetMenu(fileName = "new HypeServiceSO", menuName = "Hype/HypeServiceSO", order = 0)]
    public class HypeServiceSO : ScriptableObject
    {
        public float MaxHype;
        public float AmountHypeDecreaseTime;
        public HypeSO PlayerHypeSO;
        public HypeSO EnemyHypeSO;
    }
}