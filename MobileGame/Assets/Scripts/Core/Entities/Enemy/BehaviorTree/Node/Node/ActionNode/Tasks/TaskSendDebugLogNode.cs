using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskSendDebugLogNode : ActionNode
    {
        private TaskSendDebugLogNodeSO _so;
        private TaskSendDebugLogNodeDataSO _data;

        public override void Evaluate()
        {
            base.Evaluate();
            Debug.Log($"{_data.MessageDebug}");
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskSendDebugLogNodeSO)nodeSO;
            _data = (TaskSendDebugLogNodeDataSO)_so.Data;
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            
        }
        
    }
}