using System;
using BehaviorTree.Nodes.Actions;

namespace BehaviorTree.SO.Actions
{
    public class TaskLookDirectionNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskLookDirectionNode);
        }
    }
}