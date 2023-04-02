using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/SendPlayerMovePointIndexNodeDataSO",
        fileName = "new T_SendPlayerMovePointIndex_Spe_Data")]
    public class TaskSendPlayerMovePointIndexNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.PlayerHandlerMovement };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskSendPlayerMovePointIndexNode);
        }
    }
}