using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Environment.MoveGrid;
using Player.Handler;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class CheckCirclesPlayerNode : ActionNode
    {
        private CheckCirclesPlayerNodeSO _so;
        private CheckCirclesPlayerNodeDataSO _data;
        private EnvironmentGridManager _environmentGridManager;
        private PlayerMovementHandler _playerMovementHandler;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (CheckCirclesPlayerNodeSO)nodeSO;
            _data = (CheckCirclesPlayerNodeDataSO)_so.Data;
        }

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            if (!_environmentGridManager.CheckIfMovePointInIsCircles(_playerMovementHandler.GetCurrentIndexMovePoint(),
                    _data.CirclesIndexes))
            {
             
                return BehaviourTreeEnums.NodeState.SUCCESS;
            }
           
            return BehaviourTreeEnums.NodeState.FAILURE;
        }

        public override void SetDependencyValues(
            Dictionary<BehaviourTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviourTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _environmentGridManager = (EnvironmentGridManager)
                externDependencyValues[BehaviourTreeEnums.TreeExternValues.EnvironmentGridManager];
            _playerMovementHandler =
                (PlayerMovementHandler)externDependencyValues[
                    BehaviourTreeEnums.TreeExternValues.PlayerHandlerMovement];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}