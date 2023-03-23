using Actions;
using HelperPSR.RemoteConfigs;
using HelperPSR.Tick;
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
   
        protected override Actions.PlayerAction GetAction()
        {
            return tauntPlayerAction;
        }

        public override void InitializeAction()
        {
            
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
          
            IInputService inputService = (IInputService)arguments[0];
            AddCondition(CheckIsInAttack);
            AddCondition(CheckIsInMovement);
            inputService.SetHold(TryMakeTauntAction, CancelTaunt);
            tauntPlayerAction.SetupAction((TickManager)arguments[1]);
            RemoteConfigManager.RegisterRemoteConfigurable(this);
        }

        private void CancelTaunt(InputAction.CallbackContext obj)
        {
            tauntPlayerAction.CancelTaunt();
        }

        public void SetRemoteConfigurableValues()
        {
            tauntPlayerAction.SO.EndTime = RemoteConfigManager.Config.GetFloat("PlayerTauntEndTime");
        }
    }
}