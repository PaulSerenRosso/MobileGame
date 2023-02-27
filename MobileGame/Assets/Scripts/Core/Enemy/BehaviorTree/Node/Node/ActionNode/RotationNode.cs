using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using DG.Tweening;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class RotationNode : ActionNode
    {
        private RotationNodeSO _rotationNodeSO;
        private RotationNodeDataSO _rotationNodeDataSO;
        private Transform _transformBoss;

        private bool _initRotation;
        private bool _rotationIsFinished;

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            if (!_initRotation)
            {
                _transformBoss.DORotate(
                    _transformBoss.eulerAngles + Vector3.up * _rotationNodeDataSO.RotationAmount,
                    _rotationNodeDataSO.TimeRotation).OnComplete(() => _rotationIsFinished = true);
                _initRotation = true;
            }
            else if (_rotationIsFinished)
            {
                _initRotation = false;
                _rotationIsFinished = false;
                return BehaviourTreeEnums.NodeState.SUCCESS;
            }

            return BehaviourTreeEnums.NodeState.FAILURE;
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _rotationNodeDataSO;
        }

        public override NodeSO GetNodeSO()
        {
            return _rotationNodeSO;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _rotationNodeSO = (RotationNodeSO)nodeSO;
            _rotationNodeDataSO = (RotationNodeDataSO)_rotationNodeSO.Data;
        }

        public override void SetDependencyValues(
            Dictionary<BehaviourTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviourTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _transformBoss = (Transform)enemyDependencyValues[BehaviourTreeEnums.TreeEnemyValues.Transform];
        }
    }
}