using System;
using System.Collections.Generic;
using Addressables;
using Attributes;
using UnityEditor;
using UnityEngine;


namespace Service.Inputs
{
    public class InputService : IInputService
    {
        public PlayerInputs PlayerInputs;

        private InputsSettingsSO settings;

        // logic swipe
        [ServiceInit]
        private void Initialize()
        {
        
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<InputsSettingsSO>("InputsSettingsSO",
                LoadInputsSettingsSO);
            PlayerInputs = new PlayerInputs();
            PlayerInputs.Enable();
        }

        private void LoadInputsSettingsSO(InputsSettingsSO so)
        {
            settings = so;
      
            EnablePlayerMap(true);
        }

        public void EnablePlayerMap(bool value)
        {
            // todo: Mouvement SO swipe
            // todo: tap pour le attack
            // todo: Press and release pour le hold pour l'emote
            // todo: l'ui rect avec image zone area
            for (int i = 0; i <  settings.AllSwipeSOAdressableName.Length; i++)
            {
                AddressableHelper.LoadAssetAsyncWithCompletionHandler<SwipeSO>(settings.AllSwipeSOAdressableName[i],
                    (so => AddSwipe(so, (delegate(Swipe swipe) {Debug.Log("test ça marche");  }))));
            }
        }


        // event de l'action -> custom input event (swipe and hold) -> action que tu veux genre bouger


        private List<Swipe> allSwipes = new List<Swipe>();

        [DependsOnService] private ITickeableSwitchableService _tickeableService;

        public void AddSwipe(SwipeSO swipeSo, Action<Swipe> successEvent)
        {
            Swipe swipe = new Swipe(swipeSo, successEvent, _tickeableService, PlayerInputs);
            allSwipes.Add(swipe);
            PlayerInputs.GenericInputs.MoveTouch.performed += swipe.SwipeUpdate;
            PlayerInputs.GenericInputs.PressTouch.performed+= swipe.StartSwipe;
            PlayerInputs.GenericInputs.ReleaseTouch.performed += swipe.EndSwipe;
            PlayerInputs.GenericInputs.MoveTouch.canceled += swipe.EndSwipe;
        }

        public void RemoveSwipe(Swipe swipeToRemoved)
        {
            allSwipes.Remove(swipeToRemoved);
            PlayerInputs.GenericInputs.MoveTouch.performed -= swipeToRemoved.SwipeUpdate;
            PlayerInputs.GenericInputs.PressTouch.performed -= swipeToRemoved.StartSwipe;
            PlayerInputs.GenericInputs.ReleaseTouch.performed -= swipeToRemoved.EndSwipe;
        }
    }
}