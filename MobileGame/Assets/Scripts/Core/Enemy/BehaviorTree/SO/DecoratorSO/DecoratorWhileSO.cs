using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTree.Nodes.Decorator;
using BehaviorTree.SO.Composite;
using UnityEngine;

namespace BehaviorTree.SO.Decorator
{
public class DecoratorWhileSO : DecoratorSO
{
    public BehaviourTreeEnums.NodeState WhileStateCondition;
    public override Type GetTypeNode()
    {
        return typeof(DecoratorWhile);
    }
}
}
