using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using DG.Tweening;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskRotationNode : ActionNode
    {
        private TaskRotationNodeSO _so;
        private TaskRotationNodeDataSO _data;
        private Transform _transformBoss;

        private bool _initRotation;
        private bool _rotationIsFinished;

        public override IEnumerator<BehaviorTreeEnums.NodeState> Evaluate()
        {
            if (!_initRotation)
            {
                _transformBoss.DORotate(
                    _transformBoss.eulerAngles + Vector3.up * _data.RotationAmount,
                    _data.TimeRotation).OnComplete(() => _rotationIsFinished = true);
                _initRotation = true;
            }
            else if (_rotationIsFinished)
            {
                _initRotation = false;
                _rotationIsFinished = false;
                yield return BehaviorTreeEnums.NodeState.SUCCESS;
            }

            yield return BehaviorTreeEnums.NodeState.FAILURE;
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskRotationNodeSO)nodeSO;
            _data = (TaskRotationNodeDataSO)_so.Data;
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _transformBoss = (Transform)enemyDependencyValues[BehaviorTreeEnums.TreeEnemyValues.Transform];
        }
    }
}