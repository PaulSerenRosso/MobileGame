using Actions;
using HelperPSR.RemoteConfigs;
using Service.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Handler
{
    public class PlayerTauntHandler : PlayerHandler, IRemoteConfigurable
    {
        [SerializeField] private MovementPlayerAction movementPlayerAction;
        [SerializeField] private AttackPlayerAction attackPlayerAction;
        [SerializeField] private TauntPlayerAction tauntPlayerAction;
        private IInputService _inputService;
        protected override Actions.PlayerAction GetAction()
        {
            return tauntPlayerAction;
        }

        public override void InitializeAction() { }

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

        private void CancelTaunt(InputAction.CallbackContext obj)
        {
            tauntPlayerAction.TryCancelTaunt();
        }

        public void SetRemoteConfigurableValues()
        {
            tauntPlayerAction.SO.EndTime = RemoteConfigManager.Config.GetFloat("PlayerTauntEndTime");
            tauntPlayerAction.SO.StartTime = RemoteConfigManager.Config.GetFloat("PlayerTauntStartTime");
            tauntPlayerAction.SO.HypeAmount = RemoteConfigManager.Config.GetFloat("PlayerTauntHypeAmount");
        }
    }
}