using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class CheckPlayerIsInPositionNode : ActionNode
    {
        private CheckPlayerIsInPositionNodeSO _so;
        private CheckPlayerIsInPositionNodeDataSO _data;
        private Transform _playerTransform;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (CheckPlayerIsInPositionNodeSO)nodeSO;
            _data = (CheckPlayerIsInPositionNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            Vector3[] movePointPositions = ((List<Vector3>)Sharer.InternValues[_so.InternValues[0].HashCode]).ToArray();
            if (movePointPositions.Length < 1)
            {
                State = BehaviorTreeEnums.NodeState.FAILURE;
                ReturnedEvent?.Invoke();
                return;
            }
            for (int i = 0; i < movePointPositions.Length; i++)
            {
                if (Vector3.Distance(_playerTransform.position, movePointPositions[i]) < _data.Radius)
                {
                    Debug.Log($"movePointPosition : {movePointPositions[i]}");
                    Debug.Log($"playerPosition: {_playerTransform.position}");
                    Debug.Log($"distance: {Vector3.Distance(_playerTransform.position, movePointPositions[i])}");
                    Debug.Log($"PlayerIsInPuddle");
                    State = BehaviorTreeEnums.NodeState.SUCCESS;
                    ReturnedEvent?.Invoke();
                    return;
                }
            }
            State = BehaviorTreeEnums.NodeState.FAILURE;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _playerTransform =
                (Transform)externDependencyValues[BehaviorTreeEnums.TreeExternValues.PlayerTransform];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}