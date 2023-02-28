using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Player.Handler;

namespace BehaviorTree.Nodes.Actions
{
    public class SendPlayerMovePointIndexNode : ActionNode
    {
        private SendPlayerMovePointIndexNodeSO _sendPlayerMovePointIndexNodeSO;
        private SendPlayerMovePointIndexNodeDataSO _sendPlayerMovePointIndexNodeDataSO;
        private PlayerMovementHandler _playerMovementHandler;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _sendPlayerMovePointIndexNodeSO = (SendPlayerMovePointIndexNodeSO)nodeSO;
            _sendPlayerMovePointIndexNodeDataSO =
                (SendPlayerMovePointIndexNodeDataSO)_sendPlayerMovePointIndexNodeSO.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _sendPlayerMovePointIndexNodeSO;
        }

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            if (Sharer.InternValues.TryAdd(_sendPlayerMovePointIndexNodeSO.PlayerMovePointIndexKey.HashCode,
                    _playerMovementHandler.GetCurrentIndexMovePoint()))
            {
                return BehaviourTreeEnums.NodeState.SUCCESS;
            }

            return BehaviourTreeEnums.NodeState.SUCCESS;
        }

        public override void SetDependencyValues(
            Dictionary<BehaviourTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviourTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _playerMovementHandler =
                (PlayerMovementHandler)externDependencyValues[
                    BehaviourTreeEnums.TreeExternValues.PlayerHandlerMovement];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _sendPlayerMovePointIndexNodeDataSO;
        }
    }
}