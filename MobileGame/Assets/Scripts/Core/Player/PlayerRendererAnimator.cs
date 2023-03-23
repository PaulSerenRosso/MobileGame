using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public partial class PlayerRenderer
    {
        [SerializeField]
        private string _dirParameterName;
        [SerializeField] private string _endMovementParameterName;
        private void OnValidate()
        {
            
        }

        private void SetDirParameter(Vector2 dir)
        {
            int dirToSend = -1;
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
            if (_animator.GetNextAnimatorStateInfo(0).length <= movementPlayerAction.GetMaxTimeMovement() - time)
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