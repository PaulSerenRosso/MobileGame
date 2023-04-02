using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public partial class PlayerRenderer
    {
        [SerializeField] private AnimationClip movementRecoveryAnimationClip;
        [SerializeField] private string _dirParameterName;
        [SerializeField] private string _endMovementParameterName;

        [SerializeField] private FloatAnimationParameter[] _endMovementTimeParameters;
        private const string EndMovementParameterBaseName = "EndMovementSpeed";
        int dirToSend = -1;
        private string currentDirName;
        [Serializable]
        struct FloatAnimationParameter
        {
            public AnimationClip animationClip;
            private float _time;
            public float GetTime => _time;
            public void SetTime(float value) => _time = value;
        }

        private void SetEndAnimationMovementSpeedAnimation()
        {
            for (int i = 0; i < _endMovementTimeParameters.Length; i++)
            { 
                _endMovementTimeParameters[i].SetTime(_animator.GetFloat(EndMovementParameterBaseName + i)*_endMovementTimeParameters[i].animationClip.length);
            }
        }

        private void SetRecoverySpeedAnimation()
        {
            _animator.SetFloat("RecoveryAnimationSpeed",
                _playerMovementHandler.GetRecoveryMovementTime() / movementRecoveryAnimationClip.length);
        }

        private void SetDirParameter(Vector2 dir)
        {
       
            switch (dir)
            {
                case var v when v == Vector2.left:
                {
                    dirToSend = 2;
                    break;
                }
                case var v when v == Vector2.right:
                {
                    dirToSend = 3;
                    break;
                }
                case var v when v == Vector2.up:
                {
                    dirToSend = 0;
                    break;
                }
                case var v when v == Vector2.down:
                {
                    dirToSend = 1;
                    break;
                }
                case var v when v == Vector2.zero:
                {
                    dirToSend = -1;
                    break;
                }
            }

            AnimSetInt(_dirParameterName, dirToSend);
        }

        private void ResetEndMovementAnimationParameter()
        {
            AnimSetBool(_endMovementParameterName, false);
            movementPlayerAction.MakeUpdateEvent += LaunchEndMovementPlayerAnimation;
        }

        private void LaunchEndMovementPlayerAnimation(float time)
        {
            if (movementPlayerAction.GetMaxTimeMovement()-_endMovementTimeParameters[dirToSend].GetTime >= movementPlayerAction.GetMaxTimeMovement() - time)
            {
                SetDirParameter(Vector2.zero);
                AnimSetBool(_endMovementParameterName, true);
                movementPlayerAction.MakeUpdateEvent -= LaunchEndMovementPlayerAnimation;
            }
        }

        public void AnimSetBool(string nameParameter, bool condition)
        {
            _animator.SetBool(nameParameter, condition);
        }

        public void AnimSetFloat(string nameParameter, float count)
        {
            _animator.SetFloat(nameParameter, count);
        }

        public void AnimSetInt(string nameParameter, int count)
        {
            _animator.SetInteger(nameParameter, count);
        }

        public void AnimPlay(string nameParameter)
        {
            _animator.Play(nameParameter);
        }
    }
}