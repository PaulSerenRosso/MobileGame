namespace BehaviorTree.Nodes.Composite
{
    public class SequenceNode : CompositeNode
    {
        public override BehaviorTreeEnums.NodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            for (var index = 0; index < Children.Count; index++)
            {
                var node = Children[index];
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
                    case BehaviorTreeEnums.NodeState.LOOP:
                        index--;
                        break;
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