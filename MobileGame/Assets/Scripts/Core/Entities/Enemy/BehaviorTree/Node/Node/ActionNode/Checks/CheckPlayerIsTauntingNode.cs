using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Player.Handler;

namespace BehaviorTree.Nodes.Actions
{
    public class CheckPlayerIsTauntingNode : ActionNode
    {
        private CheckPlayerIsTauntingNodeSO _so;
        private CheckPlayerIsTauntingNodeDataSO _data;
        private PlayerTauntHandler _playerTauntHandler;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (CheckPlayerIsTauntingNodeSO)nodeSO;
            _data = (CheckPlayerIsTauntingNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            State = _playerTauntHandler.CheckIsTaunting()
                ? BehaviorTreeEnums.NodeState.FAILURE
                : BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _playerTauntHandler =
                (PlayerTauntHandler)externDependencyValues[
                    BehaviorTreeEnums.TreeExternValues.PlayerTauntHandler];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}