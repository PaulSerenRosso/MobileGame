using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Player.Handler;

namespace BehaviorTree.Nodes.Actions
{
    public class CheckPlayerIsDodgingNode : ActionNode
    {
        private CheckPlayerIsDodgingNodeSO _so;
        private CheckPlayerIsDodgingNodeDataSO _data;
        private PlayerMovementHandler _playerMovementHandler;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (CheckPlayerIsDodgingNodeSO)nodeSO;
            _data = (CheckPlayerIsDodgingNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            State = _playerMovementHandler.GetIsDodging()
                ? BehaviorTreeEnums.NodeState.SUCCESS
                : BehaviorTreeEnums.NodeState.FAILURE;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _playerMovementHandler =
                (PlayerMovementHandler)externDependencyValues[BehaviorTreeEnums.TreeExternValues.PlayerMovementHandler];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}