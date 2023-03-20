﻿using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskActivationFXNode : ActionNode
    {
        private TaskActivationFXNodeSO _so;
        private TaskActivationFXNodeDataSO _data;
        private ParticleSystem[] _particleSystems;
        
        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskActivationFXNodeSO)nodeSO;
            _data = (TaskActivationFXNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            return BehaviourTreeEnums.NodeState.SUCCESS;
        }

        public override void SetDependencyValues(
            Dictionary<BehaviourTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviourTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _particleSystems = new ParticleSystem[enemyDependencyValues.Count];
            int count = 0;
            foreach (var enemyDependencyValue in enemyDependencyValues)
            {
                _particleSystems[count] = (ParticleSystem)enemyDependencyValue.Value;
                count++;
            }
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}