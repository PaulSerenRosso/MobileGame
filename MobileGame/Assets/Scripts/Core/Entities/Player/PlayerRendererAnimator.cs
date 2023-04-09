using System;
using UnityEngine;

namespace Player
{
    public partial class PlayerRenderer
    {
        [SerializeField] private AnimationClip _movementRecoveryAnimationClip;
        [SerializeField] private FloatAnimationParameter[] _endMovementTimeParameters;
        [SerializeField] private string _dirParameterName;
        [SerializeField] private string _endMovementParameterName;

        private const string EndMovementParameterBaseName = "EndMovementSpeed";
        private int _dirToSend = -1;
        private string _currentDirName;
        
        [Serializable]
        private struct FloatAnimationParameter
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
                _playerMovementHandler.GetRecoveryMovementTime() / _movementRecoveryAnimationClip.length);
        }

        private void SetDirParameter(Vector2 dir)
        {
       
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
            AnimSetBool(_endMovementParameterName, false);
            _movementPlayerAction.MakeUpdateEvent += LaunchEndMovementPlayerAnimation;
        }

        private void LaunchEndMovementPlayerAnimation(float time)
        {
            if (_movementPlayerAction.GetMaxTimeMovement()-_endMovementTimeParameters[_dirToSend].GetTime >= _movementPlayerAction.GetMaxTimeMovement() - time)
            {
                SetDirParameter(Vector2.zero);
                AnimSetBool(_endMovementParameterName, true);
                _movementPlayerAction.MakeUpdateEvent -= LaunchEndMovementPlayerAnimation;
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