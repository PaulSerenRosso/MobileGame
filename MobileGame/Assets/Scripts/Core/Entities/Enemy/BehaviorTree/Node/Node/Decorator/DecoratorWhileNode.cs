using System;
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

        public override IEnumerator<BehaviorTreeEnums.NodeState> Evaluate()
        {
            var childEvaluate = Child.Evaluate();
            childEvaluate.MoveNext();
            Func<bool> condition = () => childEvaluate.Current != _so.WhileStateCondition;
            yield return _so.WhileStateCondition;
        }
    }
}