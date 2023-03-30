using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.Nodes.Decorator
{
    public class DecoratorInverterNode : DecoratorNode
    {
        public override IEnumerator Evaluate()
        {
          CoroutineLauncher.StartCoroutine(Child.Evaluate());
          
            if (Child.State == BehaviorTreeEnums.NodeState.RUNNING)
            {
                State = BehaviorTreeEnums.NodeState.RUNNING;
                yield break;
            }
            if (Child.State == BehaviorTreeEnums.NodeState.BLOCKED)
            {
                State = BehaviorTreeEnums.NodeState.BLOCKED;
                yield break;
            }
            State = Child.State == BehaviorTreeEnums.NodeState.FAILURE
                ? BehaviorTreeEnums.NodeState.SUCCESS
                : BehaviorTreeEnums.NodeState.FAILURE;
        }
    }
}