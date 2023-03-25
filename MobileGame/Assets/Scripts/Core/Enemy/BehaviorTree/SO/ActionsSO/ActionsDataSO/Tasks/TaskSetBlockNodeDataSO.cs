using System;
using BehaviorTree.Nodes.Actions;

namespace BehaviorTree.SO.Actions
{
    public class TaskSetBlockNodeDataSO : ActionNodeDataSO
    {
        public EnemyEnums.EnemyBlockingState EnemyBlockingState;
        
        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviorTreeEnums.TreeEnemyValues.EnemyManager };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskSetBlockNode);
        }
    }
}