using System;
using BehaviorTree.Nodes.Actions;

namespace BehaviorTree.SO.Actions
{
    public class TaskLookDirectionNodeDataSO : ActionNodeDataSO
    {
        public float TimeRotation;
        
        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviorTreeEnums.TreeEnemyValues.Transform };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskLookDirectionNode);
        }
    }
}