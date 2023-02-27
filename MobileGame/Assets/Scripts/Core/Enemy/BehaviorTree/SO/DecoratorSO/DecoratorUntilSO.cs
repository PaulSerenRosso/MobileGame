using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTree.Nodes;
using BehaviorTree.Nodes.Decorator;
using BehaviorTree.SO.Decorator;
using UnityEngine;

namespace BehaviorTree.SO.Decorator
{
    [CreateAssetMenu(menuName = "BehaviorTree/Decorator/DecoratorUntilSO", fileName = "new DecoratorUntilSO")]
public class DecoratorUntilSO : DecoratorSO
{
    public BehaviourTreeEnums.NodeState BreakStateCondition;
    public override Type GetTypeNode()
    {
        return typeof(DecoratorUntil);
    }
}
}
