using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Environment.MoveGrid;
using HelperPSR.Collections;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskGetPlayerDirectionNode : ActionNode
    {
        private TaskGetPlayerDirectionNodeSO _so;
        private TaskGetPlayerDirectionNodeDataSO _data;
        private EnvironmentGridManager _environmentGridManager;
        private Transform _transform;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskGetPlayerDirectionNodeSO)nodeSO;
            _data = (TaskGetPlayerDirectionNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            int playerMovePointIndex = (int)Sharer.InternValues[_so.InternValues[0].HashCode];
            Vector3 direction = _environmentGridManager.MovePoints[playerMovePointIndex].LocalPosition -
                                _transform.position;

            CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[1].HashCode, direction.normalized);

            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _environmentGridManager =
                (EnvironmentGridManager)externDependencyValues[
                    BehaviorTreeEnums.TreeExternValues.EnvironmentGridManager];
            _transform = (Transform)enemyDependencyValues[BehaviorTreeEnums.TreeEnemyValues.Transform];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}