using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using BehaviorTree.Nodes;
using BehaviorTree.SO.Decorator;
using UnityEngine;

namespace BehaviorTree.Nodes.Decorator
{
    public class DecoratorUntil : DecoratorNode
    {
        public DecoratorUntil()
        {
            
        }
        private DecoratorWhileSO so;

        public override NodeSO GetNodeSO()
        {
            return so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            so = (DecoratorWhileSO)nodeSO;
        }

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            while (Child.Evaluate() != so.WhileStateCondition)
            {
                
            }
            return so.WhileStateCondition;
        }
    }
}