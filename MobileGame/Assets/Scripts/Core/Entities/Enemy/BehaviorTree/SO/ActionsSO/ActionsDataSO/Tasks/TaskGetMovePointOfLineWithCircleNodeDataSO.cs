using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/GetMovePointOfLineWithCircleNodeDataSO",
        fileName = "new T_GetMovePointOfLineWithCircle_Spe_Data")]
    public class TaskGetMovePointOfLineWithCircleNodeDataSO : ActionNodeDataSO
    {
        public int CircleIndex;

        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.GridManager };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskGetMovePointOfLineWithCircleNode);
        }
    }
}