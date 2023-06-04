using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/ActivateVibrationNodeDataSO",
        fileName = "new Tree_T_ActivateVibration_Spe_Data")]
    public class TaskActivateVibrationNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues() { }

        public override Type GetTypeNode()
        {
            return typeof(TaskActivateVibrationNode);
        }
    }
}