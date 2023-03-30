using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Environment.MoveGrid;
using Player.Handler;

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

        public override void Evaluate()
        {
            State = !_environmentGridManager.CheckIfMovePointInIsCircles(
                _playerMovementHandler.GetCurrentIndexMovePoint(),
                _data.CirclesIndexes)
                ? BehaviorTreeEnums.NodeState.SUCCESS
                : BehaviorTreeEnums.NodeState.FAILURE;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _environmentGridManager = (EnvironmentGridManager)
                externDependencyValues[BehaviorTreeEnums.TreeExternValues.EnvironmentGridManager];
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