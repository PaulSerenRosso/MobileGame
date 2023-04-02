using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskSetStateMobilityNode : ActionNode
    {
        private TaskSetStateMobilityNodeSO _so;
        private TaskSetStateMobilityNodeDataSO _data;
        private EnemyManager _enemyManager;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskSetStateMobilityNodeSO)nodeSO;
            _data = (TaskSetStateMobilityNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            _enemyManager.CurrentMobilityState = _data.EnemyMobilityState;
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _enemyManager = (EnemyManager)enemyDependencyValues[BehaviorTreeEnums.TreeEnemyValues.EnemyManager];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}