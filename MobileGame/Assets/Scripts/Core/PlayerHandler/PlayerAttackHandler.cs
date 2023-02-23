using Action;
using HelperPSR.Pool;
using HelperPSR.RemoteConfigs;
using HelperPSR.Tick;
using Service.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Handler
{
    public class PlayerAttackHandler : PlayerHandler<AttackAction>, IRemoteConfigurable
    {
        [SerializeField] private MovementAction _movementAction;
        [SerializeField] private TauntAction _tauntAction;
        private const string _punchName = "PlayerPunch";
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

        private bool CheckIsInTaunt()
        {
            return !_tauntAction.IsInAction;
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
            AddCondition(CheckIsInTaunt);
            _action.SetupAction((TickManager)arguments[1]);
            _action.InitCancelAttackEvent += () => _movementAction.MakeActionEvent += _action.AttackTimer.Cancel;
            _action.InitBeforeHitEvent += () => _movementAction.MakeActionEvent -= _action.AttackTimer.Cancel;
            RemoteConfigManager.RegisterRemoteConfigurable(this);
        }

        public void SetRemoteConfigurableValues()
        {
            for (int i = 0; i < _action.attackActionSo.HitsSO.Length; i++)
            {
                SetPlayerPunchSO(_action.attackActionSo.HitsSO[i],i);
            }
            
        }
        public void SetPlayerPunchSO(HitSO punchSO, int hitCount)
        {
            punchSO.Damage = RemoteConfigManager.Config.GetFloat(_punchName + hitCount + "Damage");
            punchSO.CancelTime = RemoteConfigManager.Config.GetFloat(_punchName + hitCount + "CancelTime");
            punchSO.TimeBeforeHit = RemoteConfigManager.Config.GetFloat(_punchName + hitCount + "TimeBeforeHit");
            punchSO.RecoveryTime = RemoteConfigManager.Config.GetFloat(_punchName + hitCount + "RecoveryTime");
            punchSO.ComboTime = RemoteConfigManager.Config.GetFloat(_punchName + hitCount + "ComboTime");
            punchSO.HitMovePointsDistance =RemoteConfigManager.Config.GetFloat(_punchName + hitCount + "HitMovePointsDistance");
        }
    }
}
