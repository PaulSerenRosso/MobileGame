using UnityEngine;

namespace Service.Hype
{
    [CreateAssetMenu(fileName = "new HypeServiceSO", menuName = "Hype/HypeServiceSO", order = 0)]
    public class HypeServiceSO : ScriptableObject
    {
        public float MinHype;
        public float MaxHype;
        public float BaseValueHype;
        public float AmountHypeDecreaseTime;
    }
}