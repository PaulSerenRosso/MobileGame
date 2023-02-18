using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Service.Inputs
{
    [Serializable]
    public class Swipe
    {
        public SwipeSO SwipeSO;
        private event Action<Swipe> _successEvent;
        private Vector2 _startPos;
        private float _minScreenDistanceSq;
        private bool _isCancel;
        private PlayerInputs _playerInput;

        public void StartSwipe(InputAction.CallbackContext ctx)
        {
            _isCancel = false;
            _startPos = _playerInput.GenericInputs.MoveTouch.ReadValue<Vector2>();
        }

        public void EndSwipe(InputAction.CallbackContext ctx)
        {
            if (_isCancel) return;

            if ((_playerInput.GenericInputs.MoveTouch.ReadValue<Vector2>() - _startPos).sqrMagnitude >=
                _minScreenDistanceSq)
            {
                if (Vector2.Dot(SwipeSO.DirectionV2,
                        (_playerInput.GenericInputs.MoveTouch.ReadValue<Vector2>() - _startPos).normalized) >
                    SwipeSO.DirectionTolerance)
                {
                    if ((ctx.time - ctx.startTime) < SwipeSO.Time)
                    {
                        _successEvent?.Invoke(this);
                    }
                }
            }

            CancelSwipe();
        }

        public void CancelSwipe()
        {
            _isCancel = true;
        }

        public Swipe(SwipeSO swipeSo, Action<Swipe> successEvent, PlayerInputs playerInput)
        {
            _playerInput = playerInput;
            SwipeSO = swipeSo;
            _successEvent = successEvent;
            _minScreenDistanceSq =
                (new Vector2(swipeSo.DirectionV2.x * Screen.width, swipeSo.DirectionV2.y * Screen.height) *
                 swipeSo.MinDistancePercentage).sqrMagnitude;
        }
    }
}