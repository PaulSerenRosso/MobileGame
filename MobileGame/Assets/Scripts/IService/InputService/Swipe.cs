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
            Debug.Log(_startPos);
        }

        public void EndSwipe(InputAction.CallbackContext ctx)
        {
            if (_isCancel) return;

            if ((_playerInput.GenericInputs.MoveTouch.ReadValue<Vector2>() - _startPos).sqrMagnitude >=
                _minScreenDistanceSq)
            {
                if (Vector2.Dot(SwipeSO.Direction,
                        (_playerInput.GenericInputs.MoveTouch.ReadValue<Vector2>() - _startPos).normalized) >
                    SwipeSO.DirectionTolerance)
                {
                    if ((ctx.time - ctx.startTime) < SwipeSO.Time)
                    {
                        Debug.Log("end swipe ");
                        _successEvent?.Invoke(this);
                    }
                }
            }

            CancelSwipe();
        }

        public void CancelSwipe()
        {
            Debug.Log("cancel swipe");
            _isCancel = true;
        }

        public Swipe(SwipeSO swipeSo, Action<Swipe> successEvent, PlayerInputs playerInput)
        {
            _playerInput = playerInput;
            SwipeSO = swipeSo;
            _successEvent = successEvent;
            _minScreenDistanceSq =
                (new Vector2(swipeSo.Direction.x * Screen.width, swipeSo.Direction.y * Screen.height) *
                 swipeSo.MinDistancePercentage).sqrMagnitude;
        }
    }
}