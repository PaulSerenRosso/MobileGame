using System;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    public abstract class ActionNodeDataSO : ScriptableObject
    {
        [Header("Ne pas modifier")] 
        public BehaviorTreeEnums.TreeExternValues[] ExternValues;
        public BehaviorTreeEnums.TreeEnemyValues[] EnemyValues;
        

        protected abstract void SetDependencyValues();

        public abstract Type GetTypeNode();
        
        public void OnValidate()
        {
            SetDependencyValues();
        }
    }
}