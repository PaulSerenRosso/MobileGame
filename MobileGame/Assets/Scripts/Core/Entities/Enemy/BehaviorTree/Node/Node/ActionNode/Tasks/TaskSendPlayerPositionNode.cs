using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using HelperPSR.Collections;
using Player.Handler;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskSendPlayerPositionNode : ActionNode
    {
        private TaskSendPlayerPositionNodeSO _so;
        private TaskSendPlayerPositionNodeDataSO _data;
        private Transform _playerTransform;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskSendPlayerPositionNodeSO)nodeSO;
            _data =
                (TaskSendPlayerPositionNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[0].HashCode,
                _playerTransform.position);
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
          _playerTransform =
                (Transform)externDependencyValues[
                    BehaviorTreeEnums.TreeExternValues.PlayerTransform];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}