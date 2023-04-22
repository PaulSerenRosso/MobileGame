using System;
using BehaviorTree.Nodes.Decorator;
using UnityEngine;

namespace BehaviorTree.SO.Decorator
{
    [CreateAssetMenu(menuName = "BehaviorTree/Decorator/UntilSO", fileName = "new Tree_D_Until_Spe")]
    public class DecoratorUntilSO : DecoratorSO
    {
        public BehaviorTreeEnums.NodeState BreakStateCondition;

        public override Type GetTypeNode()
        {
            return typeof(DecoratorUntilNode);
        }

        public override void UpdateComment()
        {
            Comment = "Continue d'évaluer le nœud jusqu'à recevoir le statut demandé";
        }
    }
}