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
            _so = (TaskLookDirectionNodeSO)nodeSO;
            _data = (TaskLookDirectionNodeDataSO)_so.Data;
        }

        public override BehaviorTreeEnums.NodeState Evaluate()
        {
            if (!_initRotation)
            {
                _transform.DOLookAt(
                    (Vector3)Sharer.InternValues[_so.InternValues[0].HashCode],
                    _data.TimeRotation).OnComplete(() => _rotationIsFinished = true);
                _initRotation = true;
            }
            else if (_rotationIsFinished)
            {
                Debug.Log("Rotate enemy finish");
                _initRotation = false;
                _rotationIsFinished = false;
                return BehaviorTreeEnums.NodeState.SUCCESS;
            }

            return BehaviorTreeEnums.NodeState.FAILURE;
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