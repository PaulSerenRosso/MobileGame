using System.Numerics;
using Actions;
using Player.Handler;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Player
{
    public partial class PlayerRenderer : MonoBehaviour
    {   
        [SerializeField] private Animator _animator;
        [SerializeField] private MovementPlayerAction movementPlayerAction;
        [SerializeField] private PlayerMovementHandler _playerMovementHandler;
        [SerializeField] private TauntPlayerAction tauntPlayerAction;
        [SerializeField] private PlayerHandlerUltimate _playerHandlerUltimate;
        [SerializeField] private ParticleSystem ultimateParticle;
 
        public void Init()
        {
            
            _playerMovementHandler.MakeActionEvent += SetDirParameter;
         movementPlayerAction.MakeActionEvent += ResetEndMovementAnimationParameter;
         _playerHandlerUltimate.ActivateUltimateEvent += ActivateUltimateFX;
         _playerHandlerUltimate.DeactivateUltimateEvent += DeactivateUltimateFX;
             SetEndAnimationMovementSpeedAnimation();
         //  SetRecoverySpeedAnimation();
         /*
         _tauntAction.MakeActionEvent += ActivateTauntFX;
         _tauntAction.CancelActionEvent += DeactivateTauntFX;
         */
        }

        void ActivateUltimateFX()
        {
            Debug.Log("test");
           ultimateParticle.gameObject.SetActive(true);
        }

        void DeactivateUltimateFX()
        {
            ultimateParticle.gameObject.SetActive(false);
        }
    }
}