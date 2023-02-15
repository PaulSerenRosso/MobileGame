using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Service.Inputs
{
    [Serializable]
    public class Swipe
    {
        private SwipeSO _swipeSO;
        private event Action<Swipe> _successEvent;
        private Vector2 startPos;
        private float timer;
        private Vector2 lastPos;
        private float minScreenDistanceSq;
        private bool isCancel;
        private PlayerInputs _playerInput;
        private ITickeableService _tickeableService;
        public void StartSwipe(InputAction.CallbackContext ctx)
        {
            isCancel = false;
            startPos = _playerInput.GenericInputs.MoveTouch.ReadValue<Vector2>();
            Debug.Log(startPos);
            lastPos = startPos;
            _tickeableService.tickEvent += TickTimer;
        }

        void TickTimer()
        {
            timer += _tickeableService.GetTickTime;
            if (timer >= _swipeSO.Time)
            {
                CancelSwipe();
                return;
            }
            if (Vector2.Dot(_swipeSO.Direction, (lastPos - startPos)) < _swipeSO.DirectionTolerance)
            {
                CancelSwipe();
            }
        }

        public void SwipeUpdate(InputAction.CallbackContext ctx)
        {
            if(isCancel) return;
            Debug.Log("swipe update");
            lastPos = ctx.ReadValue<Vector2>();
        }

        public void EndSwipe(InputAction.CallbackContext ctx)
        {
            Debug.Log("end swipe ");
            if ((lastPos - startPos).sqrMagnitude >= minScreenDistanceSq)
            {
                _successEvent?.Invoke(this);
            }
            CancelSwipe();
        }

        public void CancelSwipe()
        {
            Debug.Log("cancel swipe");
            isCancel = true;
            _tickeableService.tickEvent -= TickTimer;
            timer = 0;
        }

        public Swipe(SwipeSO swipeSo, Action<Swipe> successEvent, ITickeableService tickeableService,
            PlayerInputs playerInput)

        {
            _playerInput = playerInput;
            _tickeableService = tickeableService;
            _swipeSO = swipeSo;
            _successEvent = successEvent;
            minScreenDistanceSq =
                (new Vector2(swipeSo.Direction.x * Screen.width, swipeSo.Direction.y * Screen.height) *
                 swipeSo.MinDistancePercentage).sqrMagnitude;
        }
    }
}