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
        [SerializeField] private MovementPlayerAction movementPlayerAction;
        [SerializeField] private float _cooldownTimeBetweenTwoMovement;
        [SerializeField] private float _dodgeTime;

        private TickTimer _recoveryTimer;
        private TickTimer _dodgeTimer;
        private IInputService _inputService;
        private bool _inCooldown;
        private bool _isDodging;
        private GridManager _gridManager;
        private Swipe _currentSwipe;
        private int _lastMovePointIndex;
        private int _currentMovePointIndex;
        private int _maxDestinationIndex;
        private MovePoint _currentMovePoint;
        private const string _swipeName = "Swipe";

        public event Action FinishRecoveryMovementEvent;
        public event Action<Vector2> MakeActionEvent;
        public event Action<Vector3> CheckIsOccupiedEvent;

        public float GetRecoveryMovementTime() => _cooldownTimeBetweenTwoMovement;

        public bool GetIsDodging() => _isDodging;

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
                    else
                    {
                        CheckIsOccupiedEvent?.Invoke(_currentSwipe.SwipeSO.DirectionV3);
                        return false;
                    }
                    break;
                }
                case var v when v == Vector2.right:
                {
                    if (_currentMovePoint.NeighborRightIndex != -1)
                        _maxDestinationIndex = _currentMovePoint.NeighborRightIndex;
                    else
                    {
                        CheckIsOccupiedEvent?.Invoke(_currentSwipe.SwipeSO.DirectionV3);
                        return false;
                    }
                    break;
                }
                case var v when v == Vector2.up:
                {
                    if (_currentMovePoint.NeighborTopIndex != -1)
                        _maxDestinationIndex = _currentMovePoint.NeighborTopIndex;
                    else
                    {
                        CheckIsOccupiedEvent?.Invoke(_currentSwipe.SwipeSO.DirectionV3);
                        return false;
                    }
                    break;
                }
                case var v when v == Vector2.down:
                {
                    if (_currentMovePoint.NeighborDownIndex != -1)
                        _maxDestinationIndex = _currentMovePoint.NeighborDownIndex;
                    else
                    {
                        CheckIsOccupiedEvent?.Invoke(_currentSwipe.SwipeSO.DirectionV3);
                        return false;
                    }
                    break;
                }
            }

            if (_gridManager.MovePoints[_maxDestinationIndex].IsOccupied)
                CheckIsOccupiedEvent?.Invoke(_currentSwipe.SwipeSO.DirectionV3);
            return !_gridManager.MovePoints[_maxDestinationIndex].IsOccupied;
        }

        public void SetCurrentMovePoint(int movePointIndex)
        {
            _currentMovePoint = _gridManager.MovePoints[movePointIndex];
            _currentMovePointIndex = movePointIndex;
        }

        public int GetCurrentIndexMovePoint()
        {
            return _currentMovePointIndex;
        }

        public int GetLastIndexMovePoint()
        {
            return _lastMovePointIndex;
        }

        private bool CheckIsOutOfRange()
        {
            if (_gridManager.CheckIfMovePointInIsCircles(_currentMovePointIndex))
            {
                if (_currentSwipe.SwipeSO.DirectionV2 == Vector2.down)
                {
                    return false;
                }
            }

            return true;
        }

        protected override bool TryRecordInput(object[] args)
        {
            if (!base.TryRecordInput(args))
            {
                if (!CheckCooldownBetweenTwoMovement())
                {
                    SendRecordAction(args);
                    return true;
                }
            }

            return false;
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
            _inputService = (IInputService)arguments[2];

            foreach (var movementSwipeSO in _allMovementSwipesSO)
            {
                _inputService.AddSwipe(movementSwipeSO, TryMakeMovementAction);
            }

            _gridManager = (GridManager)arguments[0];
            _currentMovePointIndex = (int)arguments[1];
            _currentMovePoint = _gridManager.MovePoints[_currentMovePointIndex];
            AddCondition(CheckCooldownBetweenTwoMovement);
            AddCondition(CheckIsMoving);
            AddCondition(CheckIsOutOfRange);
            AddCondition(CheckIsInTaunt);
            AddCondition(CheckIsOccupied);
            AddCondition(CheckIsInAttack);
            _recoveryTimer = new TickTimer(_cooldownTimeBetweenTwoMovement, (TickManager)arguments[3]);
            _recoveryTimer.TickEvent += FinishCooldown;
            _recoveryTimer.InitiateEvent += LaunchCooldownBetweenTwoMovement;
            // _dodgeTimer = new TickTimer(_dodgeTime, (TickManager)arguments[3]);
            // _dodgeTimer.TickEvent += FinishDodging;
            // _dodgeTimer.InitiateEvent += LaunchDodge;
            // movementPlayerAction.MakeActionEvent += _dodgeTimer.Initiate;
            movementPlayerAction.ReachDestinationEvent += _recoveryTimer.Initiate;
            FinishRecoveryMovementEvent += CheckActionsBlockedRecord;
            GetAction().SetupAction(_currentMovePoint.MeshRenderer.transform.position);
        }

        private void LaunchDodge()
        {
            _isDodging = true;
        }

        private void FinishDodging()
        {
            _isDodging = false;
        }

        public override void Unlink()
        {
            base.Unlink();
            foreach (var movementSwipeSO in _allMovementSwipesSO)
            {
                _inputService.RemoveSwipe(movementSwipeSO);
            }

            MakeActionEvent = null;
            FinishRecoveryMovementEvent = null;
            _recoveryTimer.ResetEvents();
            _recoveryTimer.Cancel();
            RemoteConfigManager.UnRegisterRemoteConfigurable(this);
        }

        public void ResetMovePoint(int indexMovePoint)
        {
            _currentMovePoint = _gridManager.MovePoints[indexMovePoint];
            _currentMovePointIndex = indexMovePoint;
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

        public bool CheckCooldownBetweenTwoMovement()
        {
            return !_inCooldown;
        }

        protected override PlayerAction GetAction()
        {
            return movementPlayerAction;
        }

        public override void InitializeAction()
        {
            _currentMovePoint = _gridManager.MovePoints[_maxDestinationIndex];
            movementPlayerAction.Destination = _currentMovePoint.MeshRenderer.transform.position;
            _lastMovePointIndex = _currentMovePointIndex;
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

        protected override bool CheckActionsBlockedCustomCondition()
        {
            return CheckCooldownBetweenTwoMovement();
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