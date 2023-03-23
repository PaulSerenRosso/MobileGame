using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class CheckStateNode : ActionNode
    {
        private CheckStateNodeSO _so;
        private CheckStateNodeDataSO _data;
        private EnemyManager _enemyManager;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (CheckStateNodeSO)nodeSO;
            _data = (CheckStateNodeDataSO)_so.Data;
        }

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            return _enemyManager.CurrentState == _data.EnemyState ? BehaviourTreeEnums.NodeState.SUCCESS : BehaviourTreeEnums.NodeState.FAILURE;
        }

        public override void SetDependencyValues(
            Dictionary<BehaviourTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviourTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _enemyManager = (EnemyManager)enemyDependencyValues[BehaviourTreeEnums.TreeEnemyValues.EnemyManager];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}