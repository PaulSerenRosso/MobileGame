using Action;
using HelperPSR.Tick;
using Service.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Handler
{
    public class PlayerTauntHandler : PlayerHandler<TauntAction>
    {

        [SerializeField] private MovementAction _movementAction;
        [SerializeField] private AttackAction _attackAction;
        public override void InitializeAction()
        {
            
        }

        void TryMakeTauntAction(InputAction.CallbackContext ctx)
        {
            TryMakeAction();
        }

        bool CheckIsInAttack()
        {
            return !_attackAction.IsInAction;
        }
        
        bool CheckIsInMovement()
        {
            return !_movementAction.IsInAction;
        }
        public override void Setup(params object[] arguments)
        {
            IInputService inputService = (IInputService)arguments[0];
            AddCondition(CheckIsInAttack);
            AddCondition(CheckIsInMovement);
            inputService.SetHold(TryMakeTauntAction, CancelTaunt);
            _action.SetupAction((TickManager)arguments[1]);
        }

        private void CancelTaunt(InputAction.CallbackContext obj)
        {
            _action.CancelTaunt();
        }
    }   
}
