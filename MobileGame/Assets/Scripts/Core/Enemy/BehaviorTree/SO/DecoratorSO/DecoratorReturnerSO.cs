using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTree.SO.Composite;
using BehaviorTree.Nodes;
using BehaviorTree.Nodes.Decorator;
using UnityEngine;

namespace BehaviorTree.SO.Decorator
{ 
    [CreateAssetMenu(menuName = "BehaviorTree/Decorator/DecoratorReturnerSO", fileName = "new DecoratorReturnerSO")]
    public class DecoratorReturnerSO : DecoratorSO
{
    public BehaviourTreeEnums.NodeState ReturnState;
    public override Type GetTypeNode()
    {
        return typeof(DecoratorReturner);
    }
}
    
}
