using Actions;
using HelperPSR.RemoteConfigs;
using HelperPSR.Tick;
using Service.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Handler
{
    public class PlayerAttackHandler : PlayerHandlerRecordable , IRemoteConfigurable
    {
        [SerializeField] private MovementPlayerAction movementPlayerAction;
        [SerializeField] private TauntPlayerAction tauntPlayerAction;

        [SerializeField] private AttackPlayerAction attackPlayerAction; 
        private const string _punchName = "PlayerPunch";

        protected override Actions.PlayerAction GetAction()
        {
            return attackPlayerAction;
        }

        public override void InitializeAction()
        {
            
        }

        public void TryMakeAttackAction(InputAction.CallbackContext ctx)
        {
            TryMakeAction(ctx);
        }

        private bool CheckIsInAttack()
        {
            return !attackPlayerAction.IsInAction;
        }

        private bool CheckIsInTaunt()
        {
            return !tauntPlayerAction.IsInAction;
        }

        private bool CheckIsInMovement()
        {
            return !movementPlayerAction.IsInAction;
        }

        public override void Setup(params object[] arguments)
        {
            base.Setup();
            var inputService = (IInputService)arguments[0];
            inputService.AddTap(TryMakeAttackAction);
            AddCondition(CheckIsInAttack);
            AddCondition(CheckIsInMovement);
            AddCondition(CheckIsInTaunt);
            attackPlayerAction.SetupAction((TickManager)arguments[1]);
            attackPlayerAction.InitCancelAttackEvent += () => movementPlayerAction.MakeActionEvent += attackPlayerAction.AttackTimer.Cancel;
            attackPlayerAction.InitBeforeHitEvent += () => movementPlayerAction.MakeActionEvent -= attackPlayerAction.AttackTimer.Cancel;
            RemoteConfigManager.RegisterRemoteConfigurable(this);
        }

        public void SetRemoteConfigurableValues()
        {
            for (int i = 0; i < attackPlayerAction.AttackActionSo.HitsSO.Length; i++)
            {
                SetPlayerPunchSO(attackPlayerAction.AttackActionSo.HitsSO[i], i);
            }
        }

        public void SetPlayerPunchSO(HitSO punchSO, int hitCount)
        {
            punchSO.Damage = RemoteConfigManager.Config.GetFloat(_punchName + hitCount + "Damage");
            punchSO.CancelTime = RemoteConfigManager.Config.GetFloat(_punchName + hitCount + "CancelTime");
            punchSO.TimeBeforeHit = RemoteConfigManager.Config.GetFloat(_punchName + hitCount + "TimeBeforeHit");
            punchSO.RecoveryTime = RemoteConfigManager.Config.GetFloat(_punchName + hitCount + "RecoveryTime");
            punchSO.ComboTime = RemoteConfigManager.Config.GetFloat(_punchName + hitCount + "ComboTime");
            punchSO.HitMovePointsDistance =
                RemoteConfigManager.Config.GetFloat(_punchName + hitCount + "HitMovePointsDistance");
        }
    }
}