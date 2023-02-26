namespace BehaviorTree.Nodes
{
    public class Selector : CompositeNode
    {
        public Selector()
        {
            
        }
        
        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            foreach (Node node in Children)
            {
                switch (node.Evaluate())
                {
                    case BehaviourTreeEnums.NodeState.FAILURE:
                        continue;
                    case BehaviourTreeEnums.NodeState.SUCCESS:
                        _state = BehaviourTreeEnums.NodeState.SUCCESS;
                        return _state;
                    case BehaviourTreeEnums.NodeState.RUNNING:
                        _state = BehaviourTreeEnums.NodeState.RUNNING;
                        return _state;
                    default:
                        continue;
                }
            }

            _state = BehaviourTreeEnums.NodeState.FAILURE;
            return _state;
        }
    }
}