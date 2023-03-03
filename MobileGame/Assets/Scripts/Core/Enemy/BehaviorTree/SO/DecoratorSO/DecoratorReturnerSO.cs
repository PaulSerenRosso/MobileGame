using System;
using BehaviorTree.Nodes.Decorator;
using UnityEngine;

namespace BehaviorTree.SO.Decorator
{
    [CreateAssetMenu(menuName = "BehaviorTree/Struct/Decorator/DecoratorReturnerSO",
        fileName = "new DecoratorReturnerSO")]
    public class DecoratorReturnerSO : DecoratorSO
    {
        public BehaviourTreeEnums.NodeState ReturnState;

        public override Type GetTypeNode()
        {
            return typeof(DecoratorReturner);
        }

        public override void UpdateCommentary()
        {
            
        }
    }
}