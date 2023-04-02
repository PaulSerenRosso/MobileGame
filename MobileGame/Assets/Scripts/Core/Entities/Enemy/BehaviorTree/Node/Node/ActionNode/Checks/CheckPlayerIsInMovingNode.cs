using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Player.Handler;

namespace BehaviorTree.Nodes.Actions
{
    public class CheckPlayerIsInMovingNode : ActionNode
    {
        private CheckPlayerIsInMovingNodeSO _so;
        private CheckPlayerIsInMovingNodeDataSO _data;
        private PlayerMovementHandler _playerMovementHandler;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (CheckPlayerIsInMovingNodeSO)nodeSO;
            _data = (CheckPlayerIsInMovingNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            State = _playerMovementHandler.CheckIsMoving()
                ? BehaviorTreeEnums.NodeState.FAILURE
                : BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _playerMovementHandler =
                (PlayerMovementHandler)externDependencyValues[
                    BehaviorTreeEnums.TreeExternValues.PlayerHandlerMovement];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}