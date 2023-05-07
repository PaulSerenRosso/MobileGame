using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/StunPlayerNodeDataSO",
        fileName = "new Tree_T_StunPlayer_Spe_Data")]
    public class TaskStunPlayerNodeDataSO : ActionNodeDataSO
    {
        public override Type GetTypeNode()
        {
            return typeof(TaskStunPlayerNode);
        }

        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.PlayerController };
        }
    }
}