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
        private GridManager _gridManager;
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
            base.Evaluate();
            State = !_gridManager.CheckIfMovePointInIsCircles(
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
            _gridManager = (GridManager)
                externDependencyValues[BehaviorTreeEnums.TreeExternValues.GridManager];
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