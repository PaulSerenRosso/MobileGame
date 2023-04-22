using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/SetBoolInternValueNodeDataSO",
        fileName = "new Tree_T_SetBoolInternValue_Spe_Data")]
    public class TaskSetBoolInternValueNodeDataSO : ActionNodeDataSO
    {
        public bool BooleanValue;
        
        protected override void SetDependencyValues()
        {
            
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskSetBoolInternValueNode);
        }
    }
}