namespace BehaviorTree.Nodes.Composite
{
    public class SequenceNode : CompositeNode
    {
        public override BehaviorTreeEnums.NodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach (Node node in Children)
            {
                switch (node.Evaluate())
                {
                    case BehaviorTreeEnums.NodeState.FAILURE:
                        _state = BehaviorTreeEnums.NodeState.FAILURE;
                        return _state;
                    case BehaviorTreeEnums.NodeState.SUCCESS:
                        continue;
                    case BehaviorTreeEnums.NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        _state = BehaviorTreeEnums.NodeState.SUCCESS;
                        return _state;
                }
            }

            _state = anyChildIsRunning ? BehaviorTreeEnums.NodeState.RUNNING : BehaviorTreeEnums.NodeState.SUCCESS;
            return _state;
        }
    }
}