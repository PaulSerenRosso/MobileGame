using Action;
using HelperPSR.Pool;
using HelperPSR.Tick;
using Service.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Handler
{
    public class PlayerAttackHandler : PlayerHandler<AttackAction>
    {
        [SerializeField] private MovementAction _movementAction;
        
        public override void InitializeAction()
        {
            
        }

        public void TryMakeAttackAction(InputAction.CallbackContext ctx)
        {
            TryMakeAction();
        }

        private bool CheckIsInAttack()
        {
            return !_action.IsInAction;
        }

        private bool CheckIsInMovement()
        {
            return !_movementAction.IsInAction;
        }
        
        public override void Setup(params object[] arguments)
        {
            var inputService = (IInputService)arguments[0];
            inputService.AddTap(TryMakeAttackAction);
            AddCondition(CheckIsInAttack);
            AddCondition(CheckIsInMovement);
            _action.SetupAction((TickManager)arguments[1]);
            _action.InitCancelAttackEvent += () => _movementAction.MakeActionEvent += _action.AttackTimer.Cancel;
            _action.InitBeforeHitEvent += () => _movementAction.MakeActionEvent -= _action.AttackTimer.Cancel;
        }
    }
}