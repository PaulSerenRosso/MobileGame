using System.Collections;
using System.Collections.Generic;
using BehaviorTree.SO.Decorator;

namespace BehaviorTree.Nodes.Decorator
{
    public class DecoratorReturnerNode : DecoratorNode
    {
        public DecoratorReturnerSO SO;

        public override NodeSO GetNodeSO()
        {
            return SO;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            SO = (DecoratorReturnerSO)nodeSO;
        }

        public override IEnumerator Evaluate()
        {
           CoroutineLauncher.StartCoroutine(Child.Evaluate());
            if (Child.State == BehaviorTreeEnums.NodeState.BLOCKED)
            {
                State = BehaviorTreeEnums.NodeState.BLOCKED; 
                yield break;
            }
            State =  SO.ReturnState;
        }
    }
}