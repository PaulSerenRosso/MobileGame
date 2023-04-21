using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/SendDebugLogNodeDataSO", fileName = "new T_SendDebugLog_Spe_Data")]
    public class TaskSendDebugLogNodeDataSO : ActionNodeDataSO
    {
        public string MessageDebug;
        
        protected override void SetDependencyValues()
        {
            
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskSendDebugLogNode);
        }
    }
}