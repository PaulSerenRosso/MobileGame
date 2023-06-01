using Actions;
using Player.Handler;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public partial class PlayerRenderer : MonoBehaviour
    {
        public Animator Animator;
        
        [SerializeField] private AttackPlayerAction _attackPlayerAction;
        [SerializeField] private MovementPlayerAction _movementPlayerAction;
        [SerializeField] private ParticleSystem _startUltimateParticle;
        [SerializeField] private ParticleSystem _ultimateParticle;
        [SerializeField] private PlayerMovementHandler _playerMovementHandler;
        [SerializeField] private PlayerUltimateHandler playerUltimateHandler;
        [SerializeField] private TauntPlayerAction _tauntPlayerAction;
        [FormerlySerializedAs("_skinnedMeshRenderers")] [SerializeField] private Renderer[] _renderers;
        
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
            _playerMovementHandler.MakeActionEvent += ActivateMovementParticle;
            _playerMovementHandler.CheckIsOccupiedEvent += ActivateInvisibleWall;
            _movementPlayerAction.EndActionEvent += DeactivateMovementParticle;
            // SetRecoverySpeedAnimation();
            // _tauntAction.MakeActionEvent += ActivateTauntFX;
            // _tauntAction.CancelActionEvent += DeactivateTauntFX;
        }

        public Renderer[] GetSkinnedMeshRenderers()
        {
            return _renderers;
        }

        private void ActivateFX()
        {
            _startUltimateParticle.gameObject.SetActive(true);
            _ultimateParticle.gameObject.SetActive(true);
        }
        private void DeactivateFX()
        {
            _startUltimateParticle.gameObject.SetActive(false);
            _ultimateParticle.gameObject.SetActive(false);
        }
    }
}