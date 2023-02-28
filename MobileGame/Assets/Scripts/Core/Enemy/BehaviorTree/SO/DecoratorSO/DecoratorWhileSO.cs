using System;
using BehaviorTree.Nodes.Decorator;
using UnityEngine;

namespace BehaviorTree.SO.Decorator
{
    [CreateAssetMenu(menuName = "BehaviorTree/Struct/Decorator/DecoratorWhileSO", fileName = "new DecoratorWhileSO")]
    public class DecoratorWhileSO : DecoratorSO
    {
        public BehaviourTreeEnums.NodeState WhileStateCondition;

        public override Type GetTypeNode()
        {
            return typeof(DecoratorWhile);
        }
    }
}