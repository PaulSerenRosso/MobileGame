using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/CheckTimerDataSO", fileName = "new CheckTimerDataSO")]
    public class CheckTimerDataSO : ActionNodeDataSO
    {
        public float Time;
        public float StartTime;

        public override Type GetTypeNode()
        {
            return typeof(CheckTimer);
        }

        protected override void SetDependencyValues()
        {
            
        }
    }
}