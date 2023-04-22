using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/GetMovePointOfCircleDataSO",
        fileName = "new Tree_T_GetMovePointOfCircle_Spe_Data")]
    public class TaskGetMovePointOfCircleNodeDataSO : ActionNodeDataSO
    {
        public int IndexMovedAmount;

        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.GridManager };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskGetMovePointOfCircleNode);
        }
    }
}