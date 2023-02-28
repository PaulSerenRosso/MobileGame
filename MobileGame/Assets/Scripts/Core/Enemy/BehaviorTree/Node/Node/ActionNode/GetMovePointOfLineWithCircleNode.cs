using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Environment.MoveGrid;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class GetMovePointOfLineWithCircleNode : ActionNode
    {
        private GetMovePointOfLineWithCircleNodeSO _getMovePointOfLineWithCircleNodeSo;
        private GetMovePointOfLineWithCircleNodeDataSO _getMovePointOfLineWithCircleNodeDataSo;
        private EnvironmentGridManager _environmentGridManager;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _getMovePointOfLineWithCircleNodeSo = (GetMovePointOfLineWithCircleNodeSO)nodeSO;
            _getMovePointOfLineWithCircleNodeDataSo = (GetMovePointOfLineWithCircleNodeDataSO)_getMovePointOfLineWithCircleNodeSo.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _getMovePointOfLineWithCircleNodeSo;
        }

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            int startIndex = (int)Sharer.InternValues[_getMovePointOfLineWithCircleNodeSo.StartIndexKey.HashCode];
            Sharer.InternValues[_getMovePointOfLineWithCircleNodeSo.ResultIndexKey.HashCode] =
                _environmentGridManager.GetIndexMovePointFromStartMovePointLineWithCircle(startIndex,
                    _getMovePointOfLineWithCircleNodeDataSo.CircleIndex);
            
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
            return _getMovePointOfLineWithCircleNodeDataSo;
        }
    }
}