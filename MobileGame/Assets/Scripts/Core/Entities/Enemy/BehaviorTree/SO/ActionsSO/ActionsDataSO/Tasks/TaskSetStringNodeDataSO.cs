using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/SetStringDataSO",
        fileName = "new Tree_T_SetString_Spe_Data")]
    public class TaskSetStringNodeDataSO : ActionNodeDataSO
    {
        public string StringValue;

        protected override void SetDependencyValues()
        {
            
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskSetStringNode);
        }
    }
}