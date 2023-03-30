using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTree.SO.Decorator;

namespace BehaviorTree.Nodes.Decorator
{
    public class DecoratorWhileNode : DecoratorNode
    {
        private DecoratorWhileSO _so;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (DecoratorWhileSO)nodeSO;
        }

        public override IEnumerator Evaluate()
        {
            CoroutineLauncher.StartCoroutine(Child.Evaluate());
             if (Child.State == _so.WhileStateCondition || Child.State == BehaviorTreeEnums.NodeState.BLOCKED)
             {
                 State = BehaviorTreeEnums.NodeState.BLOCKED;
             }
             else
             {
                 State = Child.State;
             }
             yield break;
        }
    }
}