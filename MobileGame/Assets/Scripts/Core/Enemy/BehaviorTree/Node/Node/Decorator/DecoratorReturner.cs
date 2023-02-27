using System.Collections;
using System.Collections.Generic;
using BehaviorTree.SO.Composite;
using BehaviorTree.Nodes;
using BehaviorTree.SO.Decorator;
using UnityEngine;

namespace BehaviorTree.Nodes.Decorator
{
    public class DecoratorReturner : DecoratorNode
    {
        public DecoratorReturnerSO so;
        public DecoratorReturner()
        {
            
        }

        public override NodeSO GetNodeSO()
        {
            return so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            so = (DecoratorReturnerSO)nodeSO;
        }

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            Child.Evaluate();
            return so.ReturnState;
        }
    }
}