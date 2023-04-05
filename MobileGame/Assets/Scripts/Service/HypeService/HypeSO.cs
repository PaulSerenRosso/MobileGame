using UnityEngine;

namespace Service.Hype
{
    [CreateAssetMenu(fileName = "new HypeSO", menuName = "Hype/HypeSO", order = 0)]
    public class HypeSO : ScriptableObject
    {
        public float StartValue;
        public float UltimateValue;
    }
}