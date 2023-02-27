using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using BehaviorTree.Nodes;
using BehaviorTree.Nodes.Decorator;
using BehaviorTree.SO.Decorator;
using UnityEngine;

namespace BehaviorTree.Nodes.Decorator
{
public class DecoratorWhile : DecoratorNode
{
    private DecoratorWhileSO so;

    public DecoratorWhile()
    {
        
    }
    public override NodeSO GetNodeSO()
    {
        return so; 
    }

    public override void SetNodeSO(NodeSO nodeSO)
    {
        so =(DecoratorWhileSO) nodeSO;
    }

    public override BehaviourTreeEnums.NodeState Evaluate()
    {
        var childEvaluate = Child.Evaluate();
        while (childEvaluate == so.WhileStateCondition)
        {
            childEvaluate = Child.Evaluate();
        }
        return childEvaluate;

    }
}
}
