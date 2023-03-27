using System;
using BehaviorTree.Nodes.Actions;

namespace BehaviorTree.SO.Actions
{
    public class TaskDamagePlayerNodeDataSO : ActionNodeDataSO
    {
        public float Damage;
        
        protected override void SetDependencyValues()
        {
            ExternValues = new[] { BehaviorTreeEnums.TreeExternValues.PlayerHealth };
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskDamagePlayerNode);
        }
    }
}