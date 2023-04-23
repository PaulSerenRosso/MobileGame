﻿using Actions;
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

        public void Init()
        {
            _playerMovementHandler.MakeActionEvent += SetDirParameter;
            _attackPlayerAction.HitAnimationEvent += AnimPlay;
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