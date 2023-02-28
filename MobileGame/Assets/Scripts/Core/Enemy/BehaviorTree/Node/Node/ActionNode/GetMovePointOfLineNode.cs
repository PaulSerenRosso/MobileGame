using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Environment.MoveGrid;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class GetMovePointOfLineNode : ActionNode
    {
        private GetMovePointOfLineNodeSO _getMovePointOfLineNodeSO;
        private GetMovePointOfLineNodeDataSO _getMovePointOfLineNodeDataSO;
        private EnvironmentGridManager _environmentGridManager;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _getMovePointOfLineNodeSO = (GetMovePointOfLineNodeSO)nodeSO;
            _getMovePointOfLineNodeDataSO = (GetMovePointOfLineNodeDataSO)_getMovePointOfLineNodeSO.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _getMovePointOfLineNodeSO;
        }

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            int startIndex = (int)Sharer.InternValues[_getMovePointOfLineNodeSO.StartIndexKey.HashCode];
            Sharer.InternValues[_getMovePointOfLineNodeSO.ResultIndexKey.HashCode] =
                _environmentGridManager.GetIndexMovePointFromStartMovePointLine(startIndex,
                    _getMovePointOfLineNodeDataSO.CircleIndex);
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
            return _getMovePointOfLineNodeDataSO;
        }
    }
}