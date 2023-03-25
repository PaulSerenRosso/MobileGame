namespace BehaviorTree.Nodes.Composite
{
    public class SelectorNode : CompositeNode
    {
        public override BehaviorTreeEnums.NodeState Evaluate()
        {
            foreach (Node node in Children)
            {
                switch (node.Evaluate())
                {
                    case BehaviorTreeEnums.NodeState.FAILURE:
                        continue;
                    case BehaviorTreeEnums.NodeState.SUCCESS:
                        _state = BehaviorTreeEnums.NodeState.SUCCESS;
                        return _state;
                    case BehaviorTreeEnums.NodeState.RUNNING:
                        _state = BehaviorTreeEnums.NodeState.RUNNING;
                        return _state;
                    default:
                        continue;
                }
            }

            _state = BehaviorTreeEnums.NodeState.FAILURE;
            return _state;
        }
    }
}