using System;
using UnityEngine;

namespace BehaviorTree.Data
{
    public abstract class ActionNodeDataSO : ScriptableObject
    {
        [Header("Ne pas Modifier")] 
        public BehaviourTreeEnums.TreeExternValues[] ExternValues;
        public BehaviourTreeEnums.TreeEnemyValues[] EnemyValues;
        public BehaviourTreeEnums.TreeInternValues[] InternValues;

        public abstract Type GetTypeNode();
    }
}