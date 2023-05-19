using System.Collections.Generic;
using System.Linq;
using BehaviorTree.SO.Actions;
using Environment.MoveGrid;
using HelperPSR.Collections;
using Player.Handler;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskGetRandomMovePointsNode : ActionNode
    {
        private TaskGetRandomMovePointsNodeSO _so;
        private TaskGetRandomMovePointsNodeDataSO _data;
        private GridManager _gridManager;
        private PlayerMovementHandler _playerMovementHandler;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskGetRandomMovePointsNodeSO)nodeSO;
            _data = (TaskGetRandomMovePointsNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            var count = (int)(float)Sharer.InternValues[_so.InternValues[0].HashCode];
            List<MovePoint> movePoints = new List<MovePoint>(_gridManager.MovePoints);
            movePoints.RemoveAt(_playerMovementHandler.GetCurrentIndexMovePoint());
            for (int i = movePoints.Count - 1; i >= 0 ; i--)
            {
                if (movePoints[i].IsOccupied) movePoints.RemoveAt(i);
            }

            movePoints.ShuffleList();
            List<Vector3> _movePointPositions = new();
            if (count > movePoints.Count) count = movePoints.Count;
            for (int i = 0; i < count; i++)
            {
                _movePointPositions.Add(movePoints[i].MeshRenderer.transform.position);
            }
            
            CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[1].HashCode, _movePointPositions);

            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _gridManager = (GridManager)externDependencyValues[BehaviorTreeEnums.TreeExternValues.GridManager];
            _playerMovementHandler =
                (PlayerMovementHandler)externDependencyValues[BehaviorTreeEnums.TreeExternValues.PlayerMovementHandler];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}