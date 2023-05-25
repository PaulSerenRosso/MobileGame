using System;
using UnityEngine;

namespace Service.Hype
{
    [CreateAssetMenu(fileName = "new HypeServiceSO", menuName = "Hype/HypeServiceSO", order = 0)]
    public class HypeServiceSO : ScriptableObject
    {
        public float MaxHype;
        public HypeSO PlayerHypeSO;
        public HypeSO EnemyHypeSO;
        [SerializeField] private float _ultimateAreaReachedHalfHypeTime;
         public float HalfHype;
      
        public float UltimateAreaReachedHalfHypeSpeed;
        private void OnValidate()
        {
            HalfHype = MaxHype / 2;
            UltimateAreaReachedHalfHypeSpeed = (PlayerHypeSO.UltimateValue-HalfHype)/_ultimateAreaReachedHalfHypeTime;
        }
    }
}