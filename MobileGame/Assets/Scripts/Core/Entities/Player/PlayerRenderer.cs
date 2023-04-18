using Actions;
using Player.Handler;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public partial class PlayerRenderer : MonoBehaviour
    {
        [FormerlySerializedAs("_animator")] public Animator Animator;
        [SerializeField] private MovementPlayerAction _movementPlayerAction;
        [SerializeField] private ParticleSystem _ultimateParticle;
        [FormerlySerializedAs("_playerHandlerUltimate")] [SerializeField] private PlayerUltimateHandler playerUltimateHandler;
        [SerializeField] private PlayerMovementHandler _playerMovementHandler;
        [SerializeField] private TauntPlayerAction _tauntPlayerAction;

        public void Init()
        {
            _playerMovementHandler.MakeActionEvent += SetDirParameter;
            _movementPlayerAction.MakeActionEvent += ResetEndMovementAnimationParameter;
            playerUltimateHandler.ActivateUltimateEvent += ActivateFX;
            playerUltimateHandler.DeactivateUltimateEvent += DeactivateFX;
            SetEndAnimationMovementSpeedAnimation();
            //  SetRecoverySpeedAnimation();
            // _tauntAction.MakeActionEvent += ActivateTauntFX;
            // _tauntAction.CancelActionEvent += DeactivateTauntFX;
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