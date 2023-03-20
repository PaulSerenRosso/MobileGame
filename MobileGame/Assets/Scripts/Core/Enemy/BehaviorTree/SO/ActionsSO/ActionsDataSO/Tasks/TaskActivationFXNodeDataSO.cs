using System;
using BehaviorTree.Nodes.Actions;

namespace BehaviorTree.SO.Actions
{
    public class TaskActivationFXNodeDataSO : ActionNodeDataSO
    {
        public BehaviourTreeEnums.TreeEnemyValues[] FXEnumValues;
        
        protected override void SetDependencyValues()
        {
            EnemyValues = FXEnumValues;
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskActivationFXNode);
        }
    }
}