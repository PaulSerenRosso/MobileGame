using System;
using Actions;
using UnityEngine;

namespace Player
{
    public partial class PlayerRenderer
    {
        [SerializeField] private AnimationClip _movementRecoveryAnimationClip;
       
        [SerializeField] private string _dirParameterName;
        [SerializeField] private string _endMovementParameterName;
        [SerializeField] private string _tauntParameterName;

        private const string EndMovementParameterBaseName = "EndMovementSpeed";
        private int _dirToSend = -1;
        private string _currentDirName;



        private void SetRecoverySpeedAnimation()
        {
            Animator.SetFloat("RecoveryAnimationSpeed",
                _playerMovementHandler.GetRecoveryMovementTime() / _movementRecoveryAnimationClip.length);
        }

        private void SetDirParameter(Vector2 dir)
        {
            // AnimSetBool(_endMovementParameterName, false);
            switch (dir)
            {
                case var v when v == Vector2.left:
                {
                    _dirToSend = 2;
                    break;
                }
                case var v when v == Vector2.right:
                {
                    _dirToSend = 3;
                    break;
                }
                case var v when v == Vector2.up:
                {
                    _dirToSend = 0;
                    break;
                }
                case var v when v == Vector2.down:
                {
                    _dirToSend = 1;
                    break;
                }
                case var v when v == Vector2.zero:
                {
                    _dirToSend = -1;
                    break;
                }
            }

            AnimSetInt(_dirParameterName, _dirToSend);
        }

        private void ResetEndMovementAnimationParameter()
        {
            //AnimSetBool(_endMovementParameterName, false);
            _movementPlayerAction.EndActionEvent += ResetMovementPlayerAnimation;
        }

        private void ResetMovementPlayerAnimation()
        {
            SetDirParameter(Vector2.zero);
            _movementPlayerAction.EndActionEvent -= ResetMovementPlayerAnimation;
        }

        private void LaunchTauntPlayerAnimation()
        {
            AnimSetBool(_tauntParameterName, true);
        }

        private void LaunchEndTauntPlayerAnimation()
        {
            AnimSetBool(_tauntParameterName, false);
        }

        public void AnimSetBool(string nameParameter, bool condition)
        {
            Animator.SetBool(nameParameter, condition);
        }

        public void AnimSetFloat(string nameParameter, float count)
        {
            Animator.SetFloat(nameParameter, count);
        }

        public void AnimSetInt(string nameParameter, int count)
        {
            Animator.SetInteger(nameParameter, count);
        }

        public void EnableAttackAnimatorParameter(HitSO hitSo)
        {
            isAttackRight = !isAttackRight;
            Animator.SetBool(isAttackRight ? attackRightAnimation : attackLeftAnimation, true);
        }

        public void PlayIdle(HitSO hitSo)
        {
            Animator.Play("Idle");
        }

        public void PlayAnimation(string nameParameter)
        {
            Animator.Play(nameParameter);
        }

        public void DisableAttackAnimatorParameter(HitSO hitSo)
        {
            Animator.SetBool(isAttackRight ? attackRightAnimation : attackLeftAnimation, false);
        }

        public void AnimSetTrigger(string nameParameter)
        {
            Animator.SetTrigger(nameParameter);
        }
    }
}