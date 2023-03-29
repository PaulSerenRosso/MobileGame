using System.Collections.Generic;

namespace BehaviorTree.Nodes.Composite
{
    public class SelectorNode : CompositeNode
    {
        public override IEnumerator<BehaviorTreeEnums.NodeState> Evaluate()
        {
            foreach (var node in Children)
            {
                IEnumerator<BehaviorTreeEnums.NodeState> state = node.Evaluate();
                state.MoveNext();
                switch (state.Current)
                {
                    case BehaviorTreeEnums.NodeState.FAILURE:
                        continue;
                    case BehaviorTreeEnums.NodeState.SUCCESS:
                        _state = BehaviorTreeEnums.NodeState.SUCCESS;
                        yield return _state;
                        yield break;
                    case BehaviorTreeEnums.NodeState.RUNNING:
                        _state = BehaviorTreeEnums.NodeState.RUNNING; 
                        yield return _state;
                        yield break;
                    default:
                        continue;
                }
            }

            _state = BehaviorTreeEnums.NodeState.FAILURE;
            yield return _state;
        }
    }
}