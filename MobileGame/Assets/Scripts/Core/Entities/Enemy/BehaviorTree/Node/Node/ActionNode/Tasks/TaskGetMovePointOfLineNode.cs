﻿using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Environment.MoveGrid;
using HelperPSR.Collections;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskGetMovePointOfLineNode : ActionNode
    {
        private TaskGetMovePointOfLineNodeSO _so;
        private TaskGetMovePointOfLineNodeDataSO _data;
        private EnvironmentGridManager _environmentGridManager;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskGetMovePointOfLineNodeSO)nodeSO;
            _data = (TaskGetMovePointOfLineNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override IEnumerator<BehaviorTreeEnums.NodeState> Evaluate()
        {
            int startIndex = (int)Sharer.InternValues[_so.InternValues[0].HashCode];
            CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[1].HashCode,
                _environmentGridManager.GetIndexMovePointFromStartMovePointLine(startIndex,
                    _data.indexMovedAmount));
            yield return BehaviorTreeEnums.NodeState.SUCCESS;
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _environmentGridManager =
                (EnvironmentGridManager)externDependencyValues[
                    BehaviorTreeEnums.TreeExternValues.EnvironmentGridManager];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}