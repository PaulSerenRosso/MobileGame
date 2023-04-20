using System;
using BehaviorTree.Nodes.Actions;
using BehaviorTree.SO.Actions;
using UnityEngine;

namespace Core.Entities.Enemy.BehaviorTree.SO.ActionsSO.ActionsDataSO.Tasks
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/SetRandomValueWithProbabilityNodeDataSO",
        fileName = "new T_SetRandomValueWithProbability_Spe_Data")]
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