using System;
using BehaviorTree.Nodes.Actions;
using UnityEngine;

namespace BehaviorTree.SO.Actions
{
    [CreateAssetMenu(menuName = "BehaviorTree/Data/Tasks/LerpHypeNodeDataSO",
        fileName = "new Tree_T_LerpHype_Spe_Data")]
    public class TaskLerpHypeNodeDataSO : ActionNodeDataSO
    {
        public bool isPlayerHype;
        protected override void SetDependencyValues()
        {
            ExternValues = new[]
            {
                BehaviorTreeEnums.TreeExternValues.HypeService
            };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskLerpHypeNode);
        }
    }
}