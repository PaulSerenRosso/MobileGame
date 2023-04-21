using System;
using BehaviorTree.Nodes.Actions;

namespace BehaviorTree.SO.Actions
{
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
            return typeof(TaskGapBetweenHypeNode);
        }
    }
}