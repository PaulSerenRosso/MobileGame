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
        
        public event Action ReachMinDistanceSwipeEvent;
        
        private event Action<Swipe> _successEvent;
        private Vector2 _startPos;
        private float _minScreenDistanceSq;
        private bool _isSuccess;
        private Vector2 _currentPos;
        private TickTimer _swipeTimer;

        public Swipe(SwipeSO swipeSo, Action<Swipe> successEvent, TickManager tickManager)
        {
            _swipeTimer = new TickTimer(swipeSo.Time, tickManager);
            _swipeTimer.TickEvent += CancelSwipe;
            SwipeSO = swipeSo;
            _successEvent = successEvent;
            _minScreenDistanceSq =
                (new Vector2(swipeSo.DirectionV2.x * Screen.width, swipeSo.DirectionV2.y * Screen.height) *
                 swipeSo.MinDistancePercentage).sqrMagnitude;
        }

        public void StartSwipe(InputAction.CallbackContext ctx)
        {
            _startPos = Input.GetTouch(0).position;
            _isSuccess = true;
            _swipeTimer.Initiate();
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
                if (Input.touchCount > 0)
                {
                    _currentPos = Input.GetTouch(0).position;
                }

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

        void CancelSwipe()
        {
            _isSuccess = false;
        }

        public void CancelSwipe(InputAction.CallbackContext obj)
        {
             CancelSwipe();
        }
    }
}