using Environnement.MoveGrid;
using System;
using System.Collections.Generic;
using Action;
using Service.Inputs;
using UnityEngine;

namespace Player.Handler
{
    public class PlayerMovementHandler : PlayerHandler<MovementAction>
    {
        [SerializeField] private MovementSO movementSO;
        [SerializeField] private List<SwipeSO> _allMovementSwipesSO;
        [SerializeField] private AttackAction _attackAction;
        
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
            return !_action.IsInAction;
        }

        private bool CheckIsOccupied()
        {
            var inverseDirection = transform.TransformDirection(_currentSwipe.SwipeSO.DirectionV3);
            var maxResult = -1f;
            _maxDestionationIndex = -1;
            foreach (var indexNeighbor in _currentMovePoint.Neighbors)
            {
                var result = Vector3.Dot(
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
                if (_currentSwipe.SwipeSO.DirectionV2 == Vector2.down)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckIsInAttack()
        {
            return !_attackAction.IsInAction || (_attackAction.IsInAction && _attackAction.IsCancelTimeOn);
        }

        public override void Setup(params object[] arguments)
        {
            var inputService = (IInputService)arguments[2];
            foreach (var movementSwipeSO in _allMovementSwipesSO)
            {
                inputService.AddSwipe(movementSwipeSO, TryMakeMovementAction);
            }
            _environmentGridManager = (EnvironmentGridManager)arguments[0];
            _index = (int)arguments[1];
            _currentMovePoint = _environmentGridManager.MovePoints[_index];
            _conditions = new List<Func<bool>>();
            AddCondition(CheckIsMoving);
            AddCondition(CheckIsOutOfRange);
            AddCondition(CheckIsOccupied);
            AddCondition(CheckIsInAttack);
            _action.SetupAction(_currentMovePoint.Position);
        }

        public override void InitializeAction()
        {
            _currentMovePoint = _environmentGridManager.MovePoints[_maxDestionationIndex];
            _action.Destination = _currentMovePoint.Position;
            _index = _maxDestionationIndex;
        }
    }
}