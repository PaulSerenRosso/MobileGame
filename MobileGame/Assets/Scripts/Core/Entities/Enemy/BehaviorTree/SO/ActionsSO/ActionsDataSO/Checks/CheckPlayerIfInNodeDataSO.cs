using System;
using BehaviorTree.Nodes.Actions;

namespace BehaviorTree.SO.Actions
{
    public class CheckPlayerIfInNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.PlayerHandlerMovement };
        }

        public override Type GetTypeNode()
        {
            return typeof(CheckPlayerIfInNode);
        }
    }
}