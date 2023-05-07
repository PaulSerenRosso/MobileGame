using System;
using HelperPSR.MonoLoopFunctions;
using HelperPSR.Tick;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Service.Inputs
{
    [Serializable]
    public class Swipe : IUpdatable
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
            UpdateManager.Register(this);
        }

        void CancelSwipe()
        {
            _isSuccess = false;
            UpdateManager.UnRegister(this);
        }
        

        public void EndSwipe()
        {
            if (_isSuccess)
            {
                _successEvent?.Invoke(this);
                CancelSwipe();
            }
        }

        public void CancelSwipe(InputAction.CallbackContext obj)
        {
             CancelSwipe();
        }

        public void OnUpdate()
        {
            if (Input.touchCount > 0) _currentPos = Input.GetTouch(0).position;
            
            if ((_currentPos - _startPos).sqrMagnitude >=
                _minScreenDistanceSq)
            {
                if (Vector2.Dot(SwipeSO.DirectionV2,
                        (_currentPos - _startPos).normalized) >
                    SwipeSO.DirectionTolerance)
                {
                  EndSwipe();
                    ReachMinDistanceSwipeEvent?.Invoke();
                    return;
                }
            
                CancelSwipe();
            }
        }
    }
}