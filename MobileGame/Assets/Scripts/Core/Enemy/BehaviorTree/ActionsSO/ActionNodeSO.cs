using System;
using BehaviorTree.InnerNode;
using UnityEngine;

namespace BehaviorTree.Actions
{
    public abstract class ActionNodeSO : NodeSO
    {
        [Header("Ne pas Modifier")] 
        public BehaviourTreeEnums.TreeExternValues[] ExternValues;
        public BehaviourTreeEnums.TreeEnemyValues[] EnemyValues;
        public BehaviourTreeEnums.TreeInternValues[] InternValues;
    }
}