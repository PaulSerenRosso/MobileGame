using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Environment.MoveGrid;
using Player.Handler;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskMoveGridNode : ActionNode
    {
        private TaskMoveGridNodeSO _taskMoveGridNodeSO;
        private TaskMoveGridNodeDataSO _taskMoveGridNodeDataSO;
        private EnvironmentGridManager _environmentGridManager;
        private PlayerMovementHandler _playerMovementHandler;

        public override NodeSO GetNodeSO()
        {
            return _taskMoveGridNodeSO;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _taskMoveGridNodeSO = (TaskMoveGridNodeSO)nodeSO;
            _taskMoveGridNodeDataSO = (TaskMoveGridNodeDataSO)_taskMoveGridNodeSO.Data;
        }

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            _environmentGridManager.MoveGrid((Vector3)Sharer.InternValues[_taskMoveGridNodeSO.DestinationKey.HashCode]);
            _playerMovementHandler.SetCurrentMovePoint((int)Sharer.InternValues[_taskMoveGridNodeSO.DestinationIndexMovePointKey.HashCode]);
            return BehaviourTreeEnums.NodeState.SUCCESS;
        }

        public override void SetDependencyValues(
            Dictionary<BehaviourTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviourTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _environmentGridManager =
                (EnvironmentGridManager)externDependencyValues[
                    BehaviourTreeEnums.TreeExternValues.EnvironmentGridManager];
            _playerMovementHandler =
                (PlayerMovementHandler)externDependencyValues[
                    BehaviourTreeEnums.TreeExternValues.PlayerHandlerMovement];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _taskMoveGridNodeDataSO;
        }
    }
}