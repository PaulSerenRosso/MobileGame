using Environment.MoveGrid;
using System;
using System.Collections.Generic;
using Action;
using HelperPSR.RemoteConfigs;
using Service.Inputs;
using UnityEngine;

namespace Player.Handler
{
    public class PlayerMovementHandler : PlayerHandler<MovementAction>, IRemoteConfigurable
    {
        [SerializeField] private MovementSO _movementSO;
        [SerializeField] private List<SwipeSO> _allMovementSwipesSO;
        [SerializeField] private AttackAction _attackAction;
        [SerializeField] private TauntAction _tauntAction;

        private EnvironmentGridManager _environmentGridManager;
        private Swipe _currentSwipe;
        private int _currentMovePointIndex;
        private int _maxDestinationIndex;
        private MovePoint _currentMovePoint;
        private const string _swipeName = "Swipe";

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
            _maxDestinationIndex = -1;
            switch (_currentSwipe.SwipeSO.DirectionV2)
            {
                case var v when v == Vector2.left:
                {
                    if (_currentMovePoint.NeighborLeftIndex != -1)
                        _maxDestinationIndex = _currentMovePoint.NeighborLeftIndex;
                    else return false;
                    break;
                }
                case var v when v == Vector2.right:
                {
                    if (_currentMovePoint.NeighborRightIndex != -1)
                        _maxDestinationIndex = _currentMovePoint.NeighborRightIndex;
                    else return false;
                    break;
                }
                case var v when v == Vector2.up:
                {
                    if (_currentMovePoint.NeighborTopIndex != -1)
                        _maxDestinationIndex = _currentMovePoint.NeighborTopIndex;
                    else return false;
                    break;
                }
                case var v when v == Vector2.down:
                {
                    if (_currentMovePoint.NeighborDownIndex != -1)
                        _maxDestinationIndex = _currentMovePoint.NeighborDownIndex;
                    else return false;
                    break;
                }
            }

            if (_environmentGridManager.MovePoints[_maxDestinationIndex].IsOccupied)
            
                return false;
            
            return true;
        }

        public void SetCurrentMovePoint(int movePointIndex)
        {
            _currentMovePoint = _environmentGridManager.MovePoints[movePointIndex];
            _currentMovePointIndex = movePointIndex;
        }

        public int GetCurrentIndexMovePoint()
        {
            return _currentMovePointIndex;
        }

        private bool CheckIsOutOfRange()
        {
            if (_environmentGridManager.CheckIfMovePointInIsCircles(_currentMovePointIndex))
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

        private bool CheckIsInTaunt()
        {
            return !_tauntAction.IsInAction;
        }

        public override void Setup(params object[] arguments)
        {
            var inputService = (IInputService)arguments[2];

            foreach (var movementSwipeSO in _allMovementSwipesSO)
            {
                inputService.AddSwipe(movementSwipeSO, TryMakeMovementAction);
            }

            _environmentGridManager = (EnvironmentGridManager)arguments[0];
            _currentMovePointIndex = (int)arguments[1];
            _currentMovePoint = _environmentGridManager.MovePoints[_currentMovePointIndex];
            _conditions = new List<Func<bool>>();
            AddCondition(CheckIsMoving);
            AddCondition(CheckIsOutOfRange);
            AddCondition(CheckIsInTaunt);
            AddCondition(CheckIsOccupied);
            AddCondition(CheckIsInAttack);
            _action.SetupAction(_currentMovePoint.MeshRenderer.transform.position);
            RemoteConfigManager.RegisterRemoteConfigurable(this);
        }

        public override void InitializeAction()
        {
            _currentMovePoint = _environmentGridManager.MovePoints[_maxDestinationIndex];
            _action.Destination = _currentMovePoint.MeshRenderer.transform.position;
            _currentMovePointIndex = _maxDestinationIndex;
        }

        public void SetRemoteConfigurableValues()
        {
            foreach (var swipeSo in _allMovementSwipesSO)
            {
                switch (swipeSo.DirectionV2)
                {
                    case var v when v.Equals(Vector2.up):
                    {
                        SetSwipeSO(swipeSo, Enums.Direction.Top);
                        break;
                    }
                    case var v when v.Equals(Vector2.down):
                    {
                        SetSwipeSO(swipeSo, Enums.Direction.Down);
                        break;
                    }
                    case var v when v.Equals(Vector2.right):
                    {
                        SetSwipeSO(swipeSo, Enums.Direction.Right);
                        break;
                    }
                    case var v when v.Equals(Vector2.left):
                    {
                        SetSwipeSO(swipeSo, Enums.Direction.Left);
                        break;
                    }
                }
            }

            _movementSO.MaxTime = RemoteConfigManager.Config.GetFloat("PlayerMovementMaxTime");
        }

        public void SetSwipeSO(SwipeSO so, Enums.Direction direction)
        {
            string dirString = "";
            switch (direction)
            {
                case Enums.Direction.Down:
                {
                    dirString = "Down";
                    break;
                }
                case Enums.Direction.Top:
                {
                    dirString = "Up";
                    break;
                }
                case Enums.Direction.Left:
                {
                    dirString = "Left";
                    break;
                }
                case Enums.Direction.Right:
                {
                    dirString = "Right";
                    break;
                }
            }

            so.Time = RemoteConfigManager.Config.GetFloat(_swipeName + dirString + "Time");
            so.MinDistancePercentage =
                RemoteConfigManager.Config.GetFloat(_swipeName + dirString + "MinDistancePercentage");
            so.DirectionTolerance = RemoteConfigManager.Config.GetFloat(_swipeName + dirString + "DirectionTolerance");
        }
    }
}