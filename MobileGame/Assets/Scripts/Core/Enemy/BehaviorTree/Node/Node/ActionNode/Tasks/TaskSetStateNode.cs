using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskSetStateNode : ActionNode
    {
        private TaskSetStateNodeSO _so;
        private TaskSetStateNodeDataSO _data;
        private EnemyManager _enemyManager;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskSetStateNodeSO)nodeSO;
            _data = (TaskSetStateNodeDataSO)_so.Data;
        }

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            Debug.Log("SetState");
            _enemyManager.CurrentState = _data.EnemyState;
            return BehaviourTreeEnums.NodeState.SUCCESS;
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