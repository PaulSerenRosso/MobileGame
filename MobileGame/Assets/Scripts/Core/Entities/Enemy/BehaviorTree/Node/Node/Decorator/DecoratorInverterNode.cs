using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.Nodes.Decorator
{
    public class DecoratorInverterNode : DecoratorNode
    {
        public override IEnumerator<BehaviorTreeEnums.NodeState> Evaluate()
        {
            var childEvaluate = Child.Evaluate();
            childEvaluate.MoveNext();
            if (childEvaluate.Current == BehaviorTreeEnums.NodeState.RUNNING)
            {
                yield return BehaviorTreeEnums.NodeState.RUNNING;
            }

            yield return childEvaluate.Current == BehaviorTreeEnums.NodeState.FAILURE
                ? BehaviorTreeEnums.NodeState.SUCCESS
                : BehaviorTreeEnums.NodeState.FAILURE;
        }
    }
}