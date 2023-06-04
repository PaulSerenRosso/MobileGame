using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using HelperPSR.MonoLoopFunctions;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskMoveNode : ActionNode, IFixedUpdate
    {
        private TaskMoveNodeSO _so;
        private TaskMoveNodeDataSO _data;
        private Rigidbody _rb;
        private bool _isInit;
        private float _ratioTime;
        private float _timer;
        private Vector3 _destination;
        private Vector3 _startPosition;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            Tree.ResetNodeList.Add(this);
            _so = (TaskMoveNodeSO)nodeSO;
            _data = (TaskMoveNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            if (!_isInit)
            {
                _destination = (Vector3)Sharer.InternValues[_so.InternValues[0].HashCode];
                FixedUpdateManager.Register(this);
                _startPosition = _rb.position;
                _isInit = true;
            }
            else if (_timer >= _data.MaxTime)
            {
                _rb.position = _destination;
                _startPosition = _destination;
                FixedUpdateManager.UnRegister(this);
                _timer = 0;
                _isInit = false;
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
            _rb.position = _destination;
            _startPosition = _destination;
            FixedUpdateManager.UnRegister(this);
            _timer = 0;
            _isInit = false;
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _rb = (Rigidbody)enemyDependencyValues[BehaviorTreeEnums.TreeEnemyValues.Rigidbody];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }

        public void OnFixedUpdate()
        {
            _timer += Time.fixedDeltaTime;
            _ratioTime = _timer / _data.MaxTime;
            _rb.position = Vector3.Lerp(_startPosition, _destination, _data.CurvePosition.Evaluate(_ratioTime));
        }
    }
}