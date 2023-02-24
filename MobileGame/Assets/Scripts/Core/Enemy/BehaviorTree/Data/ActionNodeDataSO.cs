using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.Data
{
    public abstract class ActionNodeDataSO : ScriptableObject
    {
        [Header("A ne pas Modifier")] public BehaviourTreeEnums.TreeExternValues[] externValues;
        public BehaviourTreeEnums.TreeEnemyValues[] enemyValues;
        public BehaviourTreeEnums.TreeInternValues[] internValues;

        public abstract Type GetTypeNode();
    }
}