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
          
            RemoteConfigManager.RegisterRemoteConfigurable(this);
            IInputService inputService = (IInputService)arguments[0];
            AddCondition(CheckIsInAttack);
            AddCondition(CheckIsInMovement);
            inputService.SetHold(TryMakeTauntAction, CancelTaunt);
            tauntPlayerAction.SetupAction(arguments[1], arguments[2]);
        }

        private void CancelTaunt(InputAction.CallbackContext obj)
        {
            tauntPlayerAction.TryCancelTaunt();
        }

        public void SetRemoteConfigurableValues()
        {
            tauntPlayerAction.SO.EndTime = RemoteConfigManager.Config.GetFloat("PlayerTauntEndTime");
            tauntPlayerAction.SO.StartTime = RemoteConfigManager.Config.GetFloat("PlayerTauntStartTime");
        }
    }
}