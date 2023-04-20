using System;
using BehaviorTree.Nodes.Actions;

namespace BehaviorTree.SO.Actions
{
    public class TaskSetIntInternValueNodeDataSO : ActionNodeDataSO
    {
        public BehaviorTreeEnums.InternValueIntCalculate InternValueIntCalculate;
        public int Value;

        protected override void SetDependencyValues()
        {
            
        }

        public override Type GetTypeNode()
        {
            return typeof(TaskSetIntInternValueNode);
        }
    }
}