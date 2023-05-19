using System;
using Actions;
using HelperPSR.RemoteConfigs;
using Service.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Handler
{
    public class PlayerTauntHandler : PlayerHandlerRecordable, IRemoteConfigurable
    {
        public event Action MakeActionEvent;
        public event Action MakeFinishActionEvent;
        
        [SerializeField] private MovementPlayerAction movementPlayerAction;
        [SerializeField] private AttackPlayerAction attackPlayerAction;
        [SerializeField] private TauntPlayerAction tauntPlayerAction;
        
        private IInputService _inputService;

        protected override PlayerAction GetAction()
        {
            return tauntPlayerAction;
        }
        
        public bool CheckIsTaunting()
        {
            return !GetAction().IsInAction;
        }

        public override void InitializeAction()
        {
            MakeActionEvent?.Invoke();
        }

        void TryMakeTauntAction(InputAction.CallbackContext ctx)
        {
            TryMakeAction(ctx);
        }

        bool CheckIsInAttack()
        {
            return !attackPlayerAction.IsInAction;
        }

        bool CheckIsInMovement()
        {
            return !movementPlayerAction.IsInAction;
        }

        public override void Setup(params object[] arguments)
        {
            base.Setup();
            RemoteConfigManager.RegisterRemoteConfigurable(this);
            _inputService = (IInputService)arguments[0];
            AddCondition(CheckIsInAttack);
            AddCondition(CheckIsInMovement);
            _inputService.SetHold(TryMakeTauntAction, CancelTaunt);
            tauntPlayerAction.SetupAction(arguments[1], arguments[2]);
        }

        public override void Unlink()
        {
            _inputService.ClearHold();
            RemoteConfigManager.UnRegisterRemoteConfigurable(this);
        }

        public void TryCancelTaunt()
        {
            tauntPlayerAction.TryCancelTaunt();
            MakeFinishActionEvent?.Invoke();
        }

        private void CancelTaunt(InputAction.CallbackContext obj)
        {
            tauntPlayerAction.TryCancelTaunt();
            MakeFinishActionEvent?.Invoke();
        }

        public void SetRemoteConfigurableValues()
        {
            tauntPlayerAction.SO.EndTime = RemoteConfigManager.Config.GetFloat("PlayerTauntEndTime");
            tauntPlayerAction.SO.StartTime = RemoteConfigManager.Config.GetFloat("PlayerTauntStartTime");
            tauntPlayerAction.SO.HypeAmount = RemoteConfigManager.Config.GetFloat("PlayerTauntHypeAmount");
        }
    }
}