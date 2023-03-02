using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Environment.MoveGrid;
using HelperPSR.Collections;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class GetMovePointOfCircleNode : ActionNode
    {
        private GetMovePointOfCircleNodeSO _so;
        private GetMovePointOfCircleNodeDataSO _data;
        private EnvironmentGridManager _environmentGridManager;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (GetMovePointOfCircleNodeSO)nodeSO;
            _data = (GetMovePointOfCircleNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            int startIndex = (int)Sharer.InternValues[_so.StartIndexKey.HashCode];
            CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.ResultIndexKey.HashCode,
                _environmentGridManager.GetIndexMovePointFromStartMovePointCircle(startIndex, _data.IndexMovedAmount));
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