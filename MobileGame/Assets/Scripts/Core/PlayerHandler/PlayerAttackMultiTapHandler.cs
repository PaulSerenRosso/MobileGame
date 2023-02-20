using System.Collections;
using System.Collections.Generic;
using Action;
using HelperPSR.Tick;
using Player.Handler;
using Service.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Handler
{
    public class PlayerAttackMultiTapHandler : PlayerHandler<AttackAction>
    {
        [SerializeField] private MovementAction _movementAction;

        private bool _attackIsQueued;
        public override void InitializeAction()
        {
        }

        public void TryMakeAttackAction(InputAction.CallbackContext ctx)
        {
            
                TryMakeAction();
            
            // si attacking
            // je passe la bool true;
            // a la fin d'une attaque faut passer la bool false
            // déclencher le relancement de l'attaque au moment où is attacking = false
        }

        private bool CheckIsInAttack()
        {
            if (_action.IsInAction)
                _attackIsQueued = true;
            return !_action.IsInAction;
        }

        private bool CheckIsInMovement()
        {
            return !_movementAction.IsInAction;
        }

        private void LaunchAttackQueued()
        {
            if (_attackIsQueued)
            {
                TryMakeAction();
                _attackIsQueued = false;
            }
        }

        private void CancelMultiTapAttackWhenMove()
        {
            _attackIsQueued = false;
            _action.AttackTimer.Cancel();
        }

        public override void Setup(params object[] arguments)
        {
            var inputService = (IInputService)arguments[0];
            inputService.AddTap(TryMakeAttackAction);
            AddCondition(CheckIsInAttack);
            AddCondition(CheckIsInMovement);
            _action.EndRecoveryEvent += LaunchAttackQueued;
            _action.SetupAction((TickManager)arguments[1]);
            _action.InitCancelAttackEvent += () => _movementAction.MakeActionEvent += CancelMultiTapAttackWhenMove;
            _action.InitBeforeHitEvent += () => _movementAction.MakeActionEvent -= CancelMultiTapAttackWhenMove;
        }
    }
}