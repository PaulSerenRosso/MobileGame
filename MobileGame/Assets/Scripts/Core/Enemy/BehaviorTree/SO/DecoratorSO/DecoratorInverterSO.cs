using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTree.SO.Composite;
using BehaviorTree.Nodes;
using BehaviorTree.Nodes.Decorator;
using UnityEngine;

namespace BehaviorTree.SO.Decorator
{
    [CreateAssetMenu(menuName = "BehaviorTree/Struct/Decorator/DecoratorInverterSO",
        fileName = "new DecoratorInverterSO")]
    public class DecoratorInverterSO : DecoratorSO
    {
        public override Type GetTypeNode()
        {
            return typeof(DecoratorInverter);
        }
    }
}