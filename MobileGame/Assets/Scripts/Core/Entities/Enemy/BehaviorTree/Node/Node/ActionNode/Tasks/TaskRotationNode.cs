using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using DG.Tweening;
using HelperPSR.MonoLoopFunctions;
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

        public override void Evaluate()
        {
            if (!_initRotation)
            {
                Vector3 direction = _transformBoss.eulerAngles + Vector3.up * _data.RotationAmount;
                _transformBoss.DORotate(
                    direction,
                    _data.TimeRotation*(Vector3.Angle(_transformBoss.forward, direction)/180)).OnComplete(() => _rotationIsFinished = true);
                _initRotation = true;
            }
            else if (_rotationIsFinished)
            {
                _initRotation = false;
                _rotationIsFinished = false;
                State = BehaviorTreeEnums.NodeState.SUCCESS;
                ReturnedEvent?.Invoke();
                return;
            }
            State = BehaviorTreeEnums.NodeState.FAILURE;
            ReturnedEvent?.Invoke();
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