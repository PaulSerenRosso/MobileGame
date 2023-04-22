using System;
using BehaviorTree.Nodes.Decorator;
using UnityEngine;

namespace BehaviorTree.SO.Decorator
{
    [CreateAssetMenu(menuName = "BehaviorTree/Decorator/WhileSO", fileName = "new Tree_D_While_Spe")]
    public class DecoratorWhileSO : DecoratorSO
    {
        public BehaviorTreeEnums.NodeState WhileStateCondition;

        public override Type GetTypeNode()
        {
            return typeof(DecoratorWhileNode);
        }

        public override void UpdateComment()
        {
            Comment = "Continue d'évaluer le nœud jusqu'à ce que le nœud retourne un statut différent que ce que demande le decorator";
        }
    }
}