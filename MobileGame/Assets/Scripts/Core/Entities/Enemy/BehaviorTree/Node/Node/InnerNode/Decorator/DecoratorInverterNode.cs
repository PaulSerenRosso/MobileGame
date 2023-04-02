namespace BehaviorTree.Nodes.Decorator
{
    public class DecoratorInverterNode : DecoratorNode
    {
        public override void Evaluate()
        {
            Child.Evaluate();
        }

        public override void EvaluateChild()
        {
            if (Child.State == BehaviorTreeEnums.NodeState.SUCCESS)
            {
                State = BehaviorTreeEnums.NodeState.FAILURE;
                ReturnedEvent?.Invoke();
            }
            else
            {
                State = BehaviorTreeEnums.NodeState.SUCCESS;
                ReturnedEvent?.Invoke();
            }
        }
    }
}