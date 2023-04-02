namespace BehaviorTree.Nodes.Composite
{
    public class SequenceNode : CompositeNode
    {
        private int _counter;
        
        public override void EvaluateChild()
        {
            var node = Children[_counter];
            switch (node.State)
            {
                case BehaviorTreeEnums.NodeState.FAILURE:
                    State = BehaviorTreeEnums.NodeState.FAILURE;
                    ReturnedEvent.Invoke();
                    break;
                case BehaviorTreeEnums.NodeState.SUCCESS:
                    if (_counter < Children.Count - 1)
                    {
                        _counter++;
                        Children[_counter].Evaluate();
                    }
                    else
                    {
                        State = BehaviorTreeEnums.NodeState.SUCCESS;
                        ReturnedEvent.Invoke();
                    }
                    break;
            }
        }

        public override void Evaluate()
        {
            _counter = 0;
            Children[0].Evaluate();
        }
    }
}