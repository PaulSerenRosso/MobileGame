﻿using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Core.Enemy.BehaviorTree.SO.ActionsSO;
using Environment.MoveGrid;
using HelperPSR.Collections;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskGetIndexMovePointPositionNode : ActionNode
    {
        private TaskGetIndexMovePointPositionNodeSO _so;
        private TaskGetIndexMovePointPositionNodeDataSO _data;
        private EnvironmentGridManager _environmentGridManager;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskGetIndexMovePointPositionNodeSO)nodeSO;
            _data = (TaskGetIndexMovePointPositionNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            int index = (int)Sharer.InternValues[_so.InternValues[0].HashCode];
            CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[1].HashCode,
                _environmentGridManager.MovePoints[index].MeshRenderer.transform.position);
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