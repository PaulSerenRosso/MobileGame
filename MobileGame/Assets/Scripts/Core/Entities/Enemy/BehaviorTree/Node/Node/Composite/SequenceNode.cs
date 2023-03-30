using System.Collections;
using System.Collections.Generic;

namespace BehaviorTree.Nodes.Composite
{
    public class SequenceNode : CompositeNode
    {
        public override IEnumerator Evaluate()
        {
            bool anyChildIsRunning = false;

            for (var index = 0; index < Children.Count; index++)
            {
                var node = Children[index];
                CoroutineLauncher.StartCoroutine(node.Evaluate());

                switch (node.State)
                {
                    case BehaviorTreeEnums.NodeState.FAILURE:
                        State = BehaviorTreeEnums.NodeState.FAILURE;
                        yield break;
                    case BehaviorTreeEnums.NodeState.SUCCESS:
                        continue;
                    case BehaviorTreeEnums.NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    case BehaviorTreeEnums.NodeState.BLOCKED:
                    {
                        index--;
                        yield return 0;
                        break;
                    }
                    default:
                        State = BehaviorTreeEnums.NodeState.SUCCESS;
                        yield break;
                }
            }

            State = anyChildIsRunning ? BehaviorTreeEnums.NodeState.RUNNING : BehaviorTreeEnums.NodeState.SUCCESS;
            yield return State;
        }
    }
}