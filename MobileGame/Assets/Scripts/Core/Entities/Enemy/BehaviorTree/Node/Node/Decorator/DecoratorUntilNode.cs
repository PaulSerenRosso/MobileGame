using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTree.SO.Decorator;
using UnityEditor.Experimental.GraphView;
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

        public override IEnumerator Evaluate()
        {
            CoroutineLauncher.StartCoroutine(Child.Evaluate());
            if (Child.State == _so.BreakStateCondition)
            {
                Child.State = _so.BreakStateCondition;
            }
            else
            {
                State = BehaviorTreeEnums.NodeState.BLOCKED;
            }
            yield break;
    }
    // premier chose là le problème 
}

}