using System;
using BehaviorTree.Nodes.Decorator;
using UnityEngine;

namespace BehaviorTree.SO.Decorator
{
    [CreateAssetMenu(menuName = "BehaviorTree/Struct/Decorator/DecoratorUntilSO", fileName = "new DecoratorUntilSO")]
    public class DecoratorUntilSO : DecoratorSO
    {
        public BehaviourTreeEnums.NodeState BreakStateCondition;

        public override Type GetTypeNode()
        {
            return typeof(DecoratorUntil);
        }

        public override void UpdateCommentary()
        {
            
        }
    }
}