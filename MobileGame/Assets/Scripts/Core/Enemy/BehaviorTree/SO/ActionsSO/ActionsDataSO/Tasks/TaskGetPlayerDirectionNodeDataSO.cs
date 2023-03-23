using System;
using BehaviorTree.Nodes.Actions;

namespace BehaviorTree.SO.Actions
{
    public class TaskGetPlayerDirectionNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskGetPlayerDirectionNode);
        }
    }
}