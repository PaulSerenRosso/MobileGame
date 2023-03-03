using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using HelperPSR.Collections;
using Player.Handler;

namespace BehaviorTree.Nodes.Actions
{
    public class SendPlayerMovePointIndexNode : ActionNode
    {
        private SendPlayerMovePointIndexNodeSO _so;
        private SendPlayerMovePointIndexNodeDataSO _data;
        private PlayerMovementHandler _playerMovementHandler;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (SendPlayerMovePointIndexNodeSO)nodeSO;
            _data =
                (SendPlayerMovePointIndexNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[0].HashCode, _playerMovementHandler.GetCurrentIndexMovePoint());

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
            return _data;
        }
    }
}