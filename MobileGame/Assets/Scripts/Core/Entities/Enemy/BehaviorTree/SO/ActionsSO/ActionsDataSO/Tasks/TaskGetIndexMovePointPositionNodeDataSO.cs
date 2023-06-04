using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/GetIndexMovePointPositionNodeDataSO",
        fileName = "new Tree_T_GetIndexMovePointPosition_Spe_Data")]
    public class TaskGetIndexMovePointPositionNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.GridManager };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskGetIndexMovePointPositionNode);
        }
    }
}