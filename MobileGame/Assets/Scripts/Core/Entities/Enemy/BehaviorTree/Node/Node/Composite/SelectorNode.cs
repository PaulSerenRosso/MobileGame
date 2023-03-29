namespace BehaviorTree.Nodes.Composite
{
    public class SelectorNode : CompositeNode
    {
        public override BehaviorTreeEnums.NodeState Evaluate()
        {
            for (var index = 0; index < Children.Count; index++)
            {
                var node = Children[index];
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
                    case BehaviorTreeEnums.NodeState.LOOP:
                        index--;
                        break;
                    default:
                        continue;
                }
            }

            _state = BehaviorTreeEnums.NodeState.FAILURE;
            return _state;
        }
    }
}