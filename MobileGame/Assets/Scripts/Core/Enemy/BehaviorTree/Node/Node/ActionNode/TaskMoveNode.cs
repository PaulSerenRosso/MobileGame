using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using HelperPSR.MonoLoopFunctions;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskMoveNode : ActionNode, IFixedUpdate
    {
        private TaskEnemyMoveNodeSO _taskEnemyMoveNodeSo;
        private TaskMoveNodeDataSO _taskMoveNodeDataSO;
        private Rigidbody _rb;
        private bool isInit;
        private float _ratioTime;
        private float _timer;
        private Vector3 _destination;
        private Vector3 _startPosition;

        public override NodeSO GetNodeSO()
        {
            return _taskEnemyMoveNodeSo;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _taskEnemyMoveNodeSo = (TaskEnemyMoveNodeSO)nodeSO;
            _taskMoveNodeDataSO = (TaskMoveNodeDataSO)_taskEnemyMoveNodeSo.Data;
        }

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            if (!isInit)
            {
                _destination = (Vector3)Sharer.InternValues[_taskEnemyMoveNodeSo.DestinationKey.HashCode];
                FixedUpdateManager.Register(this);
                _startPosition = _rb.position;
                isInit = true;
            }
            else if (_timer >= _taskMoveNodeDataSO.MaxTime)
            {
                _rb.position = _destination;
                FixedUpdateManager.UnRegister(this);
                isInit = false;
                return BehaviourTreeEnums.NodeState.SUCCESS;
            }
            return BehaviourTreeEnums.NodeState.RUNNING;
        }

        public override void SetDependencyValues(
            Dictionary<BehaviourTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviourTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _rb = (Rigidbody)enemyDependencyValues[BehaviourTreeEnums.TreeEnemyValues.Rigidbody];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _taskMoveNodeDataSO;
        }

        public void OnFixedUpdate()
        {
            _timer += Time.fixedDeltaTime;
            _ratioTime = _timer / _taskMoveNodeDataSO.MaxTime;
            _rb.position = Vector3.Lerp(_startPosition, _destination,_taskMoveNodeDataSO.CurvePosition.Evaluate(_ratioTime));
        }
    }
}