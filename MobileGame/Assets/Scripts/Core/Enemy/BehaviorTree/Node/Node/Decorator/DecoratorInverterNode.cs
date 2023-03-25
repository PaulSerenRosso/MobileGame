namespace BehaviorTree.Nodes.Decorator
{
    public class DecoratorInverterNode : DecoratorNode
    {
        public override BehaviorTreeEnums.NodeState Evaluate()
        {
            var childEvaluate = Child.Evaluate();
            if (childEvaluate == BehaviorTreeEnums.NodeState.RUNNING)
            {
                return BehaviorTreeEnums.NodeState.RUNNING;
            }

            return childEvaluate == BehaviorTreeEnums.NodeState.FAILURE
                ? BehaviorTreeEnums.NodeState.SUCCESS
                : BehaviorTreeEnums.NodeState.FAILURE;
        }
    }
}