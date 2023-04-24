using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
        [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/SendPlayerPositionNodeDataSO",
            fileName = "new Tree_T_SendPlayerPositionIndex_Spe_Data")]
    public class TaskSendPlayerPositionNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
            {
                ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.PlayerTransform };
            }

            public override Type GetTypeNode()
            {
                return typeof(TaskSendPlayerPositionNode);
            }
        
    }
}