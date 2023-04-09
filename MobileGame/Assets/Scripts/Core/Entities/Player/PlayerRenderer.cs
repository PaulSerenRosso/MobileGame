using Actions;
using Player.Handler;
using UnityEngine;

namespace Player
{
    public partial class PlayerRenderer : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private MovementPlayerAction _movementPlayerAction;
        [SerializeField] private ParticleSystem _ultimateParticle;
        [SerializeField] private PlayerHandlerUltimate _playerHandlerUltimate;
        [SerializeField] private PlayerMovementHandler _playerMovementHandler;
        [SerializeField] private TauntPlayerAction _tauntPlayerAction;

        public void Init()
        {
            _playerMovementHandler.MakeActionEvent += SetDirParameter;
            _movementPlayerAction.MakeActionEvent += ResetEndMovementAnimationParameter;
            _playerHandlerUltimate.ActivateUltimateEvent += ActivateUltimateFX;
            _playerHandlerUltimate.DeactivateUltimateEvent += DeactivateUltimateFX;
            SetEndAnimationMovementSpeedAnimation();
            //  SetRecoverySpeedAnimation();
            // _tauntAction.MakeActionEvent += ActivateTauntFX;
            // _tauntAction.CancelActionEvent += DeactivateTauntFX;
        }

        void ActivateUltimateFX()
        {
            _ultimateParticle.gameObject.SetActive(true);
        }

        void DeactivateUltimateFX()
        {
            _ultimateParticle.gameObject.SetActive(false);
        }
    }
}