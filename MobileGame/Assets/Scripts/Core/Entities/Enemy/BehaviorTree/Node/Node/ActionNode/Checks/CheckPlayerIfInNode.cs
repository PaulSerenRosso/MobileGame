using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Player.Handler;

namespace BehaviorTree.Nodes.Actions
{
    public class CheckPlayerIfInNode : ActionNode
    {
        private CheckPlayerIfInNodeSO _so;
        private CheckPlayerIfInNodeDataSO _data;

        private PlayerMovementHandler _playerMovementHandler;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (CheckPlayerIfInNodeSO)nodeSO;
            _data = (CheckPlayerIfInNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            int attackIndex = (int)Sharer.InternValues[_so.InternValues[0].HashCode];
            State = _playerMovementHandler.GetCurrentIndexMovePoint() == attackIndex
                ? BehaviorTreeEnums.NodeState.SUCCESS
                : BehaviorTreeEnums.NodeState.FAILURE;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _playerMovementHandler =
                (PlayerMovementHandler)externDependencyValues[BehaviorTreeEnums.TreeExternValues.PlayerHandlerMovement];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}