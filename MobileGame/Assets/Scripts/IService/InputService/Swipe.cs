using System;
using HelperPSR.Tick;
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
        private bool _isSuccess;
        private PlayerInputs _playerInput;
        private Vector2 _currentPos;
        private TickTimer _swipeTimer;
        public event Action ReachMinDistanceSwipeEvent;

        public void StartSwipe(InputAction.CallbackContext ctx)
        {
            _startPos = Input.GetTouch(0).position;
            _isSuccess = true;
            _swipeTimer.Initiate();
        }

        void CancelSwipe()
        {
            _isSuccess = false;
        }

        public void UpdateSwipe(InputAction.CallbackContext ctx)
        {
            // _currentPos = ctx.ReadValue<Vector2>();
            //
            // if ((_currentPos - _startPos).sqrMagnitude >=
            //     _minScreenDistanceSq)
            // {
            //     if (Vector2.Dot(SwipeSO.DirectionV2,
            //             (_currentPos - _startPos).normalized) >
            //         SwipeSO.DirectionTolerance)
            //     {
            //         _isSuccess = true;
            //         ReachMinDistanceSwipeEvent?.Invoke();
            //         return;
            //     }
            //
            //     CancelSwipe();
            // }
        }

        public void EndSwipe(InputAction.CallbackContext ctx)
        {
            if (_isSuccess)
            {
                _currentPos = Input.GetTouch(0).position;
                Debug.Log((_currentPos - _startPos).sqrMagnitude);
               
                if ((_currentPos - _startPos).sqrMagnitude >=
                    _minScreenDistanceSq)
                {
                    if (Vector2.Dot(SwipeSO.DirectionV2,
                            (_currentPos - _startPos).normalized) >
                        SwipeSO.DirectionTolerance)
                    {
                        ReachMinDistanceSwipeEvent?.Invoke();
                    }
                    else CancelSwipe();
                }
                else CancelSwipe();
                if (_isSuccess)
                {
                    _successEvent?.Invoke(this);
                }

                CancelSwipe();
            }
        }

        public Swipe(SwipeSO swipeSo, Action<Swipe> successEvent, PlayerInputs playerInput, TickManager tickManager)
        {
            _playerInput = playerInput;
            _swipeTimer = new TickTimer(swipeSo.Time, tickManager);
            _swipeTimer.TickEvent += CancelSwipe;
            SwipeSO = swipeSo;
            _successEvent = successEvent;
            _minScreenDistanceSq =
                (new Vector2(swipeSo.DirectionV2.x * Screen.width, swipeSo.DirectionV2.y * Screen.height) *
                 swipeSo.MinDistancePercentage).sqrMagnitude;
        }

        public void CancelSwipe(InputAction.CallbackContext obj)
        {
             CancelSwipe();
        }
    }
}