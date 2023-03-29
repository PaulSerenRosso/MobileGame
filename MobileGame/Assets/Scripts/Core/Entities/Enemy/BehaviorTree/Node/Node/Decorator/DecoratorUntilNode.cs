using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTree.SO.Decorator;
using UnityEngine;

namespace BehaviorTree.Nodes.Decorator
{
    public class DecoratorUntilNode : DecoratorNode
    {
        private DecoratorUntilSO _so;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (DecoratorUntilSO)nodeSO;
        }
        
        public override IEnumerator<BehaviorTreeEnums.NodeState> Evaluate()
        {
            var childEvaluate = Child.Evaluate();
            childEvaluate.MoveNext();
            Func<bool> condition = () => childEvaluate.Current == _so.BreakStateCondition;
            yield return _so.BreakStateCondition;
        }
    }
}