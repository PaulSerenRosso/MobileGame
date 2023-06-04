using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using DG.Tweening;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskLookDirectionNode : ActionNode
    {
        private TaskLookDirectionNodeSO _so;
        private TaskLookDirectionNodeDataSO _data;

        private Transform _transform;
        private bool _initRotation;
        private bool _rotationIsFinished;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            Tree.ResetNodeList.Add(this);
            _so = (TaskLookDirectionNodeSO)nodeSO;
            _data = (TaskLookDirectionNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            if (!_initRotation)
            {
                Vector3 playerPosition = (Vector3)Sharer.InternValues[_so.InternValues[0].HashCode];
                _transform.DOLookAt(playerPosition,
                        _data.TimeRotation *
                        (Vector3.Angle(_transform.forward, playerPosition - _transform.position) / 180))
                    .OnComplete(() => _rotationIsFinished = true);
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

        public override void Reset()
        {
            base.Reset();
            _transform.DOKill();
            _initRotation = false;
            _rotationIsFinished = false;
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _transform = (Transform)enemyDependencyValues[BehaviorTreeEnums.TreeEnemyValues.Transform];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}