using Actions;
using Player.Handler;
using UnityEngine;

namespace Player
{
    public partial class PlayerRenderer : MonoBehaviour
    {
        public Animator Animator;
        
        [SerializeField] private AttackPlayerAction _attackPlayerAction;
        [SerializeField] private MovementPlayerAction _movementPlayerAction;
        [SerializeField] private ParticleSystem _ultimateParticle;
        [SerializeField] private PlayerMovementHandler _playerMovementHandler;
        [SerializeField] private PlayerUltimateHandler playerUltimateHandler;
        [SerializeField] private TauntPlayerAction _tauntPlayerAction;
        [SerializeField] private SkinnedMeshRenderer[] _skinnedMeshRenderers;

        public void Init()
        {
            _playerMovementHandler.MakeActionEvent += SetDirParameter;
            _attackPlayerAction.MakeActionAnimationEvent += EnableAttackAnimatorParameter;
            _attackPlayerAction.MakeActionAnimationEvent += PlayIdle;
            _attackPlayerAction.CancelAnimationEvent += DisableAttackAnimatorParameter;
            _tauntPlayerAction.MakeActionEvent += LaunchTauntPlayerAnimation;
            _tauntPlayerAction.EndActionEvent += LaunchEndTauntPlayerAnimation;
            _movementPlayerAction.MakeActionEvent += ResetEndMovementAnimationParameter;
            playerUltimateHandler.ActivateUltimateEvent += ActivateFX;
            playerUltimateHandler.DeactivateUltimateEvent += DeactivateFX;
            SetEndAnimationMovementSpeedAnimation();
              //SetRecoverySpeedAnimation();
            // _tauntAction.MakeActionEvent += ActivateTauntFX;
            // _tauntAction.CancelActionEvent += DeactivateTauntFX;
        }

        public SkinnedMeshRenderer[] GetSkinnedMeshRenderers()
        {
            return _skinnedMeshRenderers;
        }

        private void ActivateFX()
        {
            _ultimateParticle.gameObject.SetActive(true);
        }
        private void DeactivateFX()
        {
            _ultimateParticle.gameObject.SetActive(false);
        }
    }
}