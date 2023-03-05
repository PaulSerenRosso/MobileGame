using System;
using BehaviorTree.Nodes.Decorator;
using UnityEngine;

namespace BehaviorTree.SO.Decorator
{
    [CreateAssetMenu(menuName = "BehaviorTree/Decorator/InverterSO",
        fileName = "new D_Inverter_Spe")]
    public class DecoratorInverterSO : DecoratorSO
    {
        public override Type GetTypeNode()
        {
            return typeof(DecoratorInverterNode);
        }

        public override void UpdateCommentary()
        {
            
        }
    }
}