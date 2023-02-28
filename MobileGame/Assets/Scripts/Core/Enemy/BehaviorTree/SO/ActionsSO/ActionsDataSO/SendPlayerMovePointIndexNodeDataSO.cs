using System;
using BehaviorTree.Nodes.Actions;

namespace BehaviorTree.SO.Actions
{
    public class SendPlayerMovePointIndexNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviourTreeEnums.TreeExternValues.PlayerHandlerMovement };
        }

        public override Type GetTypeNode()
        {
            return typeof(SendPlayerMovePointIndexNode);
        }
    }
}