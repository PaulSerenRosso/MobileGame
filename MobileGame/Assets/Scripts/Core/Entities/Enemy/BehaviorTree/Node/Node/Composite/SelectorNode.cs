using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace BehaviorTree.Nodes.Composite
{
    public class SelectorNode : CompositeNode
    {
        public override IEnumerator Evaluate()
        {
            for (var index = 0; index < Children.Count; index++)
            {
                var node = Children[index];
                Debug.Log(CoroutineLauncher);
                Debug.Log(node);
                CoroutineLauncher.StartCoroutine(node.Evaluate());
                switch (node.State)
                {
                    case BehaviorTreeEnums.NodeState.FAILURE:
                        continue;
                    case BehaviorTreeEnums.NodeState.SUCCESS:
                    {
                        State = BehaviorTreeEnums.NodeState.SUCCESS;
                        yield break;
                    }
                    case BehaviorTreeEnums.NodeState.RUNNING:
                    {
                        State = BehaviorTreeEnums.NodeState.RUNNING;
                        yield break;
                    }
                    case BehaviorTreeEnums.NodeState.BLOCKED:
                    {
                        index--;
                        yield return 0;
                        break;
                    }
                }
            }

            State = BehaviorTreeEnums.NodeState.FAILURE;
            yield break;
        }
    }
}