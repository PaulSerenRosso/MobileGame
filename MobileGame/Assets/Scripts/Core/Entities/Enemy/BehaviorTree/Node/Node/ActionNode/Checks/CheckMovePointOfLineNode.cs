using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Environment.MoveGrid;

namespace BehaviorTree.Nodes.Actions
{
    public class CheckMovePointOfLineNode : ActionNode
    {
        private CheckMovePointOfLineNodeSO _so;
        private CheckMovePointOfLineNodeDataSO _data;
        private GridManager _gridManager;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (CheckMovePointOfLineNodeSO)nodeSO;
            _data = (CheckMovePointOfLineNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            State = _gridManager.CompareIndexMovePoints((int)Sharer.InternValues[_so.InternValues[0].HashCode],
                (int)Sharer.InternValues[_so.InternValues[1].HashCode])
                ? BehaviorTreeEnums.NodeState.SUCCESS
                : BehaviorTreeEnums.NodeState.FAILURE;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _gridManager =
                (GridManager)externDependencyValues[
                    BehaviorTreeEnums.TreeExternValues.GridManager];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}