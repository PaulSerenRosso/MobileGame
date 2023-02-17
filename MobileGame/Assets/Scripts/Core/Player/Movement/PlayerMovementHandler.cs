using System.Linq;
using Environnement.MoveGrid;
using System;
using System.Collections.Generic;
using Service.Fight;
using Service.Inputs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player.Movement
{
    public class PlayerMovementHandler : PlayerHandler<MovementAction>
    {
        [FormerlySerializedAs("_playerMovementSO")] [SerializeField] private MovementSO movementSo;
        private EnvironmentGridManager _environmentGridManager;
        private Swipe _currentSwipe;
        private int _index;
        private int _maxDestionationIndex;
        private MovePoint _currentMovePoint;

        public void TryMakeMovementAction(Swipe swipe)
        {
            _currentSwipe = swipe;
            TryMakeAction();
        }

        private bool CheckIsMoving()
        {
            return _action.IsMoving();
        }

        private bool CheckIsOccupied()
        {
            var inverseDirection = transform.InverseTransformDirection(_currentSwipe.SwipeSO.Direction);
            var maxResult = -1f;
            _maxDestionationIndex = -1;
            foreach (var indexNeighbor in _currentMovePoint.Neighbors)
            {
                var result = Vector2.Dot(
                    (_environmentGridManager.MovePoints[indexNeighbor].Position - _currentMovePoint.Position)
                    .normalized, inverseDirection
                );
                if (result >= maxResult)
                {
                    maxResult = result;
                    _maxDestionationIndex = indexNeighbor;
                }
            }

            if (_environmentGridManager.MovePoints[_maxDestionationIndex].IsOccupied) return false;
            return true;
        }

        private bool CheckIsOutOfRange()
        {
            if (_environmentGridManager.CheckIfMovePointInIsLastCircle(_index))
            {
                if (_currentSwipe.SwipeSO.Direction == Vector2.down)
                {
                    return false;
                }
            }

            return true;
        }

        public override void Setup(params object[] arguments)
        {
            // Move our champion when the player swipe
            _environmentGridManager = (EnvironmentGridManager)arguments[0];
            _index = (int)arguments[1];
            Debug.Log($"index: {_index}");
            Debug.Log($"Count: {_environmentGridManager.MovePoints.Length}");
            _currentMovePoint = _environmentGridManager.MovePoints[_index];
            _action.MovementSo = movementSo;
            _conditions = new List<Func<bool>>();
            AddCondition(CheckIsMoving);
            AddCondition(CheckIsOccupied);
            AddCondition(CheckIsOutOfRange);
            _action.Destination = _currentMovePoint.Position;
            _action.MoveToDestination();
        }

        public override void InitializeAction()
        {

            _currentMovePoint = _environmentGridManager.MovePoints[_maxDestionationIndex];
            _action.Destination = _currentMovePoint.Position;
            _index = _maxDestionationIndex;
        }
    }
}