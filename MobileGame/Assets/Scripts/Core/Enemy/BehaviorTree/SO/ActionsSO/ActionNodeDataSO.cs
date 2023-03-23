using System;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    public abstract class ActionNodeDataSO : ScriptableObject
    {
        [Header("Ne pas modifier")] 
        public BehaviourTreeEnums.TreeExternValues[] ExternValues;
        public BehaviourTreeEnums.TreeEnemyValues[] EnemyValues;
        

        protected abstract void SetDependencyValues();

        public abstract Type GetTypeNode();
        
        public void OnValidate()
        {
            SetDependencyValues();
        }
    }
}