using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Checks/TimerDataSO", fileName = "new Tree_CH_Timer_Spe_Data")]
    public class CheckTimerNodeDataSO : ActionNodeDataSO
    {
        public float Time;
        public float StartTime;
        public bool IsSendResetTimerFunction;
        public bool IsInternTimer;
        
        public override Type GetTypeNode()
        {
            return typeof(CheckTimerNode);
        }

        protected override void SetDependencyValues()
        {
            
        }
    }
}