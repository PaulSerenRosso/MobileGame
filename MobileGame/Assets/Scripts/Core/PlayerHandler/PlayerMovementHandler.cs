using Environnement.MoveGrid;
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
        private int _index;
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
                    if (_currentMovePoint.neighborLeftIndex != -1)
                        _maxDestinationIndex = _currentMovePoint.neighborLeftIndex;
                    else return false;
                    break;
                }
                case var v when v == Vector2.right:
                {
                    if (_currentMovePoint.neighborRightIndex != -1)
                        _maxDestinationIndex = _currentMovePoint.neighborRightIndex;
                    else return false;
                    break;
                }
                case var v when v == Vector2.up:
                {
                    if (_currentMovePoint.neighborTopIndex != -1)
                        _maxDestinationIndex = _currentMovePoint.neighborTopIndex;
                    else return false;
                    break;
                }
                case var v when v == Vector2.down:
                {
                    if (_currentMovePoint.neighborDownIndex != -1)
                        _maxDestinationIndex = _currentMovePoint.neighborDownIndex;
                    else return false;
                    break;
                }
            }
            
            if (_environmentGridManager.MovePoints[_maxDestinationIndex].IsOccupied) return false;
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
        _index = (int)arguments[1];
        _currentMovePoint = _environmentGridManager.MovePoints[_index];
        _conditions = new List<Func<bool>>();
        AddCondition(CheckIsMoving);
        AddCondition(CheckIsOutOfRange);
        AddCondition(CheckIsInTaunt);
        AddCondition(CheckIsOccupied);
        AddCondition(CheckIsInAttack);
        _action.SetupAction(_currentMovePoint.Position);
        RemoteConfigManager.RegisterRemoteConfigurable(this);
    }

    public override void InitializeAction()
    {
        _currentMovePoint = _environmentGridManager.MovePoints[_maxDestinationIndex];
        _action.Destination = _currentMovePoint.Position;
        _index = _maxDestinationIndex;
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