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

            if (childEvaluate == BehaviorTreeEnums.NodeState.LOOP)
            {
                return BehaviorTreeEnums.NodeState.LOOP;
            }

            return childEvaluate == BehaviorTreeEnums.NodeState.FAILURE
                ? BehaviorTreeEnums.NodeState.SUCCESS
                : BehaviorTreeEnums.NodeState.FAILURE;
        }
    }
}