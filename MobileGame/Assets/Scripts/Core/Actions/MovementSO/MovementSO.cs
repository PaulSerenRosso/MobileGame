using System;
using HelperPSR.RemoteConfig;
using UnityEngine;

namespace Action
{
    [CreateAssetMenu(menuName = "MovementActionSO", fileName = "new MovementAction")]
    public class MovementSO : ScriptableObject
    {
        public float MaxTime;
        public AnimationCurve CurvePosition;

        private void OnEnable()
        {
          
        }
    }
}