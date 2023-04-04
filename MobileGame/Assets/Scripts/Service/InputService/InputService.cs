using System;
using System.Collections.Generic;
using Attributes;
using UnityEngine.InputSystem;

namespace Service.Inputs
{
    public class InputService : IInputService
    {
        public PlayerInputs PlayerInputs;

        [DependsOnService] private ITickeableService _tickeableService;

        private List<Swipe> _allSwipes = new();

        private bool successHold;
        private bool canHold;
        private Action<InputAction.CallbackContext> successEventHold;
        private Action<InputAction.CallbackContext> successEventReleaseHold;

        [ServiceInit]
        private void Initialize()
        {
            PlayerInputs = new PlayerInputs();
            PlayerInputs.Enable();
            PlayerInputs.GenericInputs.PressTouch.performed += ActivateCanHold;
            PlayerInputs.GenericInputs.PressTouch.canceled += DeactivateCanHold;
            PlayerInputs.GenericInputs.HoldTouch.performed += TryHold;
            PlayerInputs.GenericInputs.HoldTouch.canceled += ReleaseHold;
        }

        void ActivateCanHold(InputAction.CallbackContext ctx)
        {
            canHold = true;
        }

        void DeactivateCanHold(InputAction.CallbackContext ctx)
        {
            canHold = false;
        }

        void DeactivateTryHold()
        {
            canHold = false;
        }

        public void AddTap(Action<InputAction.CallbackContext> successEvent)
        {
            PlayerInputs.GenericInputs.TapTouch.performed += successEvent;
        }

        public void RemoveTap(Action<InputAction.CallbackContext> successEvent)
        {
            PlayerInputs.GenericInputs.TapTouch.performed -= successEvent;
        }

        public void AddSwipe(SwipeSO swipeSo, Action<Swipe> successEvent)
        {
            Swipe swipe = new Swipe(swipeSo, successEvent, PlayerInputs, _tickeableService.GetTickManager);
            _allSwipes.Add(swipe);
            PlayerInputs.GenericInputs.PressTouch.performed += swipe.StartSwipe;
            PlayerInputs.GenericInputs.PressTouch.canceled += swipe.EndSwipe;
            swipe.ReachMinDistanceSwipeEvent += DeactivateTryHold;
            PlayerInputs.GenericInputs.HoldTouch.performed += swipe.CancelSwipe;
        }


        public void RemoveSwipe(Swipe swipeToRemoved)
        {
            _allSwipes.Remove(swipeToRemoved);
            PlayerInputs.GenericInputs.PressTouch.performed-= swipeToRemoved.StartSwipe;
            // remove hold cancel
            PlayerInputs.GenericInputs.PressTouch.canceled -= swipeToRemoved.EndSwipe;
        }

        public void SetHold(Action<InputAction.CallbackContext> successHoldEvent,
            Action<InputAction.CallbackContext> successCancelHoldEvent)
        {
            successEventHold = successHoldEvent;
            successEventReleaseHold = successCancelHoldEvent;
        }

        private void TryHold(InputAction.CallbackContext obj)
        {
            if (canHold)
            {
                successHold = true;
                successEventHold?.Invoke(obj);
            }
            else
            {
                successHold = false;
            }
        }

        private void ReleaseHold(InputAction.CallbackContext obj)
        {
            if (successHold)
                successEventReleaseHold?.Invoke(obj);
            successHold = false;
        }
    }
}