using System;
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

        public override void UpdateCommentary()
        {
            
        }
    }
}