using Environment.MoveGrid;
using System;
using System.Collections.Generic;
using Actions;
using HelperPSR.RemoteConfigs;
using HelperPSR.Tick;
using Service.Inputs;
using UnityEngine;

namespace Player.Handler
{
    public class PlayerMovementHandler : PlayerHandlerRecordable, IRemoteConfigurable
    {
        [SerializeField] private MovementSO _movementSO;
        [SerializeField] private List<SwipeSO> _allMovementSwipesSO;
        [SerializeField] private AttackPlayerAction attackPlayerAction;
        [SerializeField] private TauntPlayerAction tauntPlayerAction;
        [SerializeField] private TickTimer _recoveryTimer;
        [SerializeField] private MovementPlayerAction movementPlayerAction;
        [SerializeField] private float _cooldownTimeBetweenTwoMovement;

        private bool _inCooldown;
        private EnvironmentGridManager _environmentGridManager;
        private Swipe _currentSwipe;
        private int _currentMovePointIndex;
        private int _maxDestinationIndex;
        private MovePoint _currentMovePoint;
        private const string _swipeName = "Swipe";

        public event Action FinishRecoveryMovementEvent;
        public event Action<Vector2> MakeActionEvent;

        public float GetRecoveryMovementTime() => _cooldownTimeBetweenTwoMovement;

        public void TryMakeMovementAction(Swipe swipe)
        {
            TryMakeAction(swipe);
        }

        protected override void TryMakeAction(params object[] args)
        {
            _currentSwipe = (Swipe)args[0];
            base.TryMakeAction(args);
        }

        public bool CheckIsMoving()
        {
            return !GetAction().IsInAction;
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

            return !_environmentGridManager.MovePoints[_maxDestinationIndex].IsOccupied;
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
            return !attackPlayerAction.IsInAction ||
                   (attackPlayerAction.IsInAction && attackPlayerAction.IsCancelTimeOn);
        }

        private bool CheckIsInTaunt()
        {
            return !tauntPlayerAction.IsInAction;
        }

        public override void Setup(params object[] arguments)
        {
            RemoteConfigManager.RegisterRemoteConfigurable(this);

            base.Setup();
            var inputService = (IInputService)arguments[2];

            foreach (var movementSwipeSO in _allMovementSwipesSO)
            {
                inputService.AddSwipe(movementSwipeSO, TryMakeMovementAction);
            }

            _environmentGridManager = (EnvironmentGridManager)arguments[0];
            _currentMovePointIndex = (int)arguments[1];
            _currentMovePoint = _environmentGridManager.MovePoints[_currentMovePointIndex];
            AddCondition(CheckCooldownBetweenTwoMovement);
            AddCondition(CheckIsMoving);
            AddCondition(CheckIsOutOfRange);
            AddCondition(CheckIsInTaunt);
            AddCondition(CheckIsOccupied);
            AddCondition(CheckIsInAttack);
            _recoveryTimer = new TickTimer(_cooldownTimeBetweenTwoMovement, (TickManager)arguments[3]);
            _recoveryTimer.TickEvent += FinishCooldown;
            _recoveryTimer.InitiateEvent += LaunchCooldownBetweenTwoMovement;
            movementPlayerAction.ReachDestinationEvent += _recoveryTimer.Initiate;
            FinishRecoveryMovementEvent += _playerHandlerRecordableManager.LaunchRecorderAction;
            GetAction().SetupAction(_currentMovePoint.MeshRenderer.transform.position);
        }

        public void ResetMovePoint(int indexMovePoint)
        {
            _currentMovePoint = _environmentGridManager.MovePoints[indexMovePoint];
            GetAction().SetupAction(_currentMovePoint.MeshRenderer.transform.position);
        }

        private void FinishCooldown()
        {
            _inCooldown = false;
            FinishRecoveryMovementEvent?.Invoke();
        }

        private void LaunchCooldownBetweenTwoMovement()
        {
            _inCooldown = true;
        }

        private bool CheckCooldownBetweenTwoMovement()
        {
            return !_inCooldown;
        }

        protected override PlayerAction GetAction()
        {
            return movementPlayerAction;
        }

        public override void InitializeAction()
        {
            _currentMovePoint = _environmentGridManager.MovePoints[_maxDestinationIndex];
            movementPlayerAction.Destination = _currentMovePoint.MeshRenderer.transform.position;
            _currentMovePointIndex = _maxDestinationIndex;
            MakeActionEvent?.Invoke(_currentSwipe.SwipeSO.DirectionV2);
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

            _cooldownTimeBetweenTwoMovement = RemoteConfigManager.Config.GetFloat("CooldownTimeBetweenTwoMovement");
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