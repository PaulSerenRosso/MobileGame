using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Environment.MoveGrid;
using HelperPSR.Collections;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskGetMovePointOfLineNode : ActionNode
    {
        private TaskGetMovePointOfLineNodeSO _so;
        private TaskGetMovePointOfLineNodeDataSO _data;
        private GridManager _gridManager;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskGetMovePointOfLineNodeSO)nodeSO;
            _data = (TaskGetMovePointOfLineNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            int startIndex = (int)Sharer.InternValues[_so.InternValues[0].HashCode];
            CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[1].HashCode,
                _gridManager.GetIndexMovePointFromStartMovePointLine(startIndex,
                    _data.indexMovedAmount));
            State = BehaviorTreeEnums.NodeState.SUCCESS;
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