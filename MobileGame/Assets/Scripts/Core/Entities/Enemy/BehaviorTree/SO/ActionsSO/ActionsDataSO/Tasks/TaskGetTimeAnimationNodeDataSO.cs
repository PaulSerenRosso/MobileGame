using System;
using BehaviorTree.Nodes.Actions;

namespace BehaviorTree.SO.Actions
{
    public class TaskGetTimeAnimationNodeDataSO : ActionNodeDataSO
    {
        protected override void SetDependencyValues()
        {
            EnemyValues = new[] { BehaviorTreeEnums.TreeEnemyValues.Animator };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskGetTimeAnimationNode);
        }
    }
}