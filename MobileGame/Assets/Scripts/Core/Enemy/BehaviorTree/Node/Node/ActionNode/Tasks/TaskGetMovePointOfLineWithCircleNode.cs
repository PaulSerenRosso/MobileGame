using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Environment.MoveGrid;
using HelperPSR.Collections;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskGetMovePointOfLineWithCircleNode : ActionNode
    {
        private TaskGetMovePointOfLineWithCircleNodeSO _so;
        private TaskGetMovePointOfLineWithCircleNodeDataSO _data;
        private EnvironmentGridManager _environmentGridManager;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskGetMovePointOfLineWithCircleNodeSO)nodeSO;
            _data = (TaskGetMovePointOfLineWithCircleNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            int startIndex = (int)Sharer.InternValues[_so.InternValues[0].HashCode];
            Debug.Log($"index: {startIndex}");
            CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[1].HashCode,
                _environmentGridManager.GetIndexMovePointFromStartMovePointLineWithCircle(startIndex,
                    _data.CircleIndex));
            return BehaviourTreeEnums.NodeState.SUCCESS;
        }

        public override void SetDependencyValues(
            Dictionary<BehaviourTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviourTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _environmentGridManager =
                (EnvironmentGridManager)externDependencyValues[
                    BehaviourTreeEnums.TreeExternValues.EnvironmentGridManager];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}