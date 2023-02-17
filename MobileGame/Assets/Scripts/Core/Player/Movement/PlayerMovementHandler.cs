using System;
using System.Collections.Generic;
using Service.Fight;
using Service.Inputs;
using UnityEngine;

namespace Player.Movement
{
    public class PlayerMovementHandler : PlayerHandler<PlayerMovementAction>
    {
        [SerializeField] private PlayerMovementSO _playerMovementSO;
        
        private EnvironmentGridManager _environmentGridManager;
        private Swipe _currentSwipe;
        private int _index;
        private int _maxDestionationIndex;

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
            foreach (var indexNeighbor in _action.CurrentMovePoint.Neighbors)
            {
                var result = Vector2.Dot(
                    (_environmentGridManager.MovePoints[indexNeighbor].Position - _action.CurrentMovePoint.Position)
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
            _action = (PlayerMovementAction)arguments[2];
            _action.CurrentMovePoint = _environmentGridManager.MovePoints[_index];
            _action.PlayerMovementSO = _playerMovementSO;
            _conditions = new List<Func<bool>>();
            AddCondition(CheckIsMoving);
            AddCondition(CheckIsOccupied);
            AddCondition(CheckIsOutOfRange);
            _action.Move();
        }

        public override void InitializeAction()
        {
            _action.CurrentSwipe = _currentSwipe;
            _action.CurrentMovePoint = _environmentGridManager.MovePoints[_maxDestionationIndex];
            _index = _maxDestionationIndex;
        }
    }
}