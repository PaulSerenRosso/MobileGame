using System;
using System.Collections.Generic;
using Addressables;
using Attributes;
using UnityEngine;

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

        // todo: event action -> custom input event (swipe and hold) -> action irt

        public void AddSwipe(SwipeSO swipeSo, Action<Swipe> successEvent)
        {
            Swipe swipe = new Swipe(swipeSo, successEvent, PlayerInputs);
            _allSwipes.Add(swipe);
            PlayerInputs.GenericInputs.PressTouch.started += swipe.StartSwipe;
            PlayerInputs.GenericInputs.PressTouch.canceled += swipe.EndSwipe;
            // PlayerInputs.GenericInputs.MoveTouch.performed += swipe.SwipeUpdate;
            // PlayerInputs.GenericInputs.ReleaseTouch.performed += swipe.EndSwipe;
        }

        public void RemoveSwipe(Swipe swipeToRemoved)
        {
            _allSwipes.Remove(swipeToRemoved);
            PlayerInputs.GenericInputs.PressTouch.started -= swipeToRemoved.StartSwipe;
            PlayerInputs.GenericInputs.PressTouch.canceled -= swipeToRemoved.EndSwipe;
            // PlayerInputs.GenericInputs.MoveTouch.performed -= swipeToRemoved.SwipeUpdate;
            // PlayerInputs.GenericInputs.ReleaseTouch.performed -= swipeToRemoved.EndSwipe;
        }
    }
}