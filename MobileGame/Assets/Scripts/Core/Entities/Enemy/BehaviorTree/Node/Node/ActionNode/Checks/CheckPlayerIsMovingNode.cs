using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Player.Handler;

namespace BehaviorTree.Nodes.Actions
{
    public class CheckPlayerIsMovingNode : ActionNode
    {
        private CheckPlayerIsMovingNodeSO _so;
        private CheckPlayerIsMovingNodeDataSO _data;
        private PlayerMovementHandler _playerMovementHandler;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (CheckPlayerIsMovingNodeSO)nodeSO;
            _data = (CheckPlayerIsMovingNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            State = _playerMovementHandler.CheckIsMoving()
                ? BehaviorTreeEnums.NodeState.SUCCESS
                : BehaviorTreeEnums.NodeState.FAILURE;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _playerMovementHandler =
                (PlayerMovementHandler)externDependencyValues[
                    BehaviorTreeEnums.TreeExternValues.PlayerMovementHandler];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}