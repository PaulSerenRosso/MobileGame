using System;
using System.Collections.Generic;
using Attributes;
using UnityEngine.InputSystem;

namespace Service.Inputs
{
    public class InputService : IInputService
    {
        public PlayerInputs PlayerInputs;
        
        private List<Swipe> _allSwipes = new();
        
        [ServiceInit]
        private void Initialize()
        {
            PlayerInputs = new PlayerInputs();
            PlayerInputs.Enable();
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
            Swipe swipe = new Swipe(swipeSo, successEvent, PlayerInputs);
            _allSwipes.Add(swipe);
            PlayerInputs.GenericInputs.PressTouch.started += swipe.StartSwipe;
            PlayerInputs.GenericInputs.PressTouch.canceled += swipe.EndSwipe;
        }

        public void RemoveSwipe(Swipe swipeToRemoved)
        {
            _allSwipes.Remove(swipeToRemoved);
            PlayerInputs.GenericInputs.PressTouch.started -= swipeToRemoved.StartSwipe;
            PlayerInputs.GenericInputs.PressTouch.canceled -= swipeToRemoved.EndSwipe;
        }
    }
}