using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/InvokeCallbackNodeDataSO",
        fileName = "new Tree_T_InvokeCallback_Spe_Data")]
    public class TaskInvokeCallbackNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
          
        }

        public override Type GetTypeNode()
        {
             return typeof(TaskInvokeCallbackNode);
        }
    }
}