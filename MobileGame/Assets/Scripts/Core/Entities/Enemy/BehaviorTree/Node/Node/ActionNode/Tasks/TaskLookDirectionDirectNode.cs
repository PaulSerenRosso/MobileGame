using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskLookDirectionDirectNode : ActionNode
    {
        private TaskLookDirectionDirectNodeSO _so;
        private TaskLookDirectionDirectNodeDataSO _data;
        private Transform _transform;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskLookDirectionDirectNodeSO)nodeSO;
            _data = (TaskLookDirectionDirectNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            Vector3 playerPosition = (Vector3)Sharer.InternValues[_so.InternValues[0].HashCode];
            _transform.rotation = Quaternion.LookRotation(playerPosition, _transform.up);
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _transform = (Transform)enemyDependencyValues[BehaviorTreeEnums.TreeEnemyValues.Transform];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}