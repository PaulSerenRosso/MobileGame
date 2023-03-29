using System.Collections.Generic;

namespace BehaviorTree.Nodes.Composite
{
    public class SequenceNode : CompositeNode
    {
        public override IEnumerator<BehaviorTreeEnums.NodeState> Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach (var node in Children)
            {
                IEnumerator<BehaviorTreeEnums.NodeState> state = node.Evaluate();
                state.MoveNext();
                switch (state.Current)
                {
                    case BehaviorTreeEnums.NodeState.FAILURE:
                        _state = BehaviorTreeEnums.NodeState.FAILURE;
                        yield return _state;
                        yield break;
                    case BehaviorTreeEnums.NodeState.SUCCESS:
                        continue;
                    case BehaviorTreeEnums.NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        _state = BehaviorTreeEnums.NodeState.SUCCESS;
                        yield return _state;
                        yield break;
                }
            }

            _state = anyChildIsRunning ? BehaviorTreeEnums.NodeState.RUNNING : BehaviorTreeEnums.NodeState.SUCCESS;
            yield return _state;
        }
    }
}