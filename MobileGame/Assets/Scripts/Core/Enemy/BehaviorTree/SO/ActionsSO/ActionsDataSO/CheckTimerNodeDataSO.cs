using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/CheckTimerDataSO", fileName = "new CheckTimerDataSO")]
    public class CheckTimerNodeDataSO : ActionNodeDataSO
    {
        public float Time;
        public float StartTime;

        public override Type GetTypeNode()
        {
            return typeof(CheckTimerNode);
        }

        protected override void SetDependencyValues()
        {
            
        }
    }
}