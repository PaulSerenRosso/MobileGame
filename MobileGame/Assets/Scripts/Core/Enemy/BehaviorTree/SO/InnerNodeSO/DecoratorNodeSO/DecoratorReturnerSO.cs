using System;
using BehaviorTree.Nodes.Decorator;
using UnityEngine;

namespace BehaviorTree.SO.Decorator
{
    [CreateAssetMenu(menuName = "BehaviorTree/Decorator/ReturnerSO",
        fileName = "new D_Returner_Spe")]
    public class DecoratorReturnerSO : DecoratorSO
    {
        public BehaviourTreeEnums.NodeState ReturnState;

        public override Type GetTypeNode()
        {
            return typeof(DecoratorReturnerNode);
        }

        public override void UpdateCommentary()
        {
            
        }
    }
}