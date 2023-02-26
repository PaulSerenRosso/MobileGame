namespace BehaviorTree.Nodes
{
    public class DecoratorInverter : DecoratorNode
    {
        public DecoratorInverter()
        {
            
        }
        
        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            if (Child.Evaluate() == BehaviourTreeEnums.NodeState.RUNNING)
            {
                Evaluate();
            }
            return Child.Evaluate() == BehaviourTreeEnums.NodeState.FAILURE ? BehaviourTreeEnums.NodeState.SUCCESS : BehaviourTreeEnums.NodeState.FAILURE;
        }
    }
}