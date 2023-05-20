using System;
using System.Collections;
using UnityEngine;

namespace Core.Entities
{
    public class GhostRenderer : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer[] _skinnedMeshRenderers;
        [SerializeField] private Animator _animator;
        [SerializeField] private string _shaderVarRef;
        [SerializeField] private float _shaderVarRate = 0.1f;
        [SerializeField] private float _shaderVarRefreshRate = 0.05f;
        
        public void AnimateMaterial(int direction)
        {
            _animator.SetInteger("Direction", direction);
            foreach (var t in _skinnedMeshRenderers)
            {
                StartCoroutine(AnimateMaterialFloat(t.material, 0));
            }
        }

        private void OnDisable()
        {
            foreach (var t in _skinnedMeshRenderers)
            {
                t.material.SetFloat(_shaderVarRef, 1);
            }

            _animator.SetInteger("Direction", -1);
        }

        private IEnumerator AnimateMaterialFloat(Material mat, float goal)
        {
            float valueToAnimate = mat.GetFloat(_shaderVarRef);
            while (valueToAnimate > goal)
            {
                valueToAnimate -= _shaderVarRate;
                mat.SetFloat(_shaderVarRef, valueToAnimate);
                yield return new WaitForSeconds(_shaderVarRefreshRate);
            }
            
        }
    }
}