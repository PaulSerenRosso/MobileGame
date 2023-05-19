using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/GetRandomMovePointsNodeDataSO",
        fileName = "new Tree_T_GetRandomMovePoints_Spe_Data")]
    public class TaskGetRandomMovePointsNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            ExternValues = new[]
            {
                BehaviorTreeEnums.TreeExternValues.GridManager, BehaviorTreeEnums.TreeExternValues.PlayerMovementHandler
            };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskGetRandomMovePointsNode);
        }
    }
}