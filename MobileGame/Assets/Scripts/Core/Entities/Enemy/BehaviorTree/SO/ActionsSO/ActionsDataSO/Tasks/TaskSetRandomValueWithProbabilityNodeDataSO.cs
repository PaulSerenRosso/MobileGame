using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/SetRandomValueWithProbabilityNodeDataSO",
        fileName = "new Tree_T_SetRandomValueWithProbability_Spe_Data")]
    public class TaskSetRandomValueWithProbabilityNodeDataSO : ActionNodeDataSO
    {
        public int[] StartProbabilitiesValues;

        protected override void SetDependencyValues()
        {
            
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskSetRandomValueWithProbabilityNode);
        }
    }
}