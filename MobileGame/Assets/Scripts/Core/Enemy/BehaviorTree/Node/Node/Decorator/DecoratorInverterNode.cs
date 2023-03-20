namespace BehaviorTree.Nodes.Decorator
{
    public class DecoratorInverterNode : DecoratorNode
    {
        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            var childEvaluate = Child.Evaluate();
            if (childEvaluate == BehaviourTreeEnums.NodeState.RUNNING)
            {
                return BehaviourTreeEnums.NodeState.RUNNING;
            }

            return childEvaluate == BehaviourTreeEnums.NodeState.FAILURE
                ? BehaviourTreeEnums.NodeState.SUCCESS
                : BehaviourTreeEnums.NodeState.FAILURE;
        }
    }
}