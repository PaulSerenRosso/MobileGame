using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Checks/InternTimerDataSO", fileName = "new CH_InternTimer_Spe_Data")]
    public class CheckInternTimerNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            
        }

        public override Type GetTypeNode()
        {
            return typeof(CheckInternTimerNode);
        }
    }
}