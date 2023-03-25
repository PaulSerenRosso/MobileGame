using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/GetMovePointOfLineNodeDataSO",
        fileName = "new T_GetMovePointOfLine_Spe_Data")]
    public class TaskGetMovePointOfLineNodeDataSO : ActionNodeDataSO
    {
        public int indexMovedAmount;
        
        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.EnvironmentGridManager };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskGetMovePointOfLineNode);
        }
    }
}