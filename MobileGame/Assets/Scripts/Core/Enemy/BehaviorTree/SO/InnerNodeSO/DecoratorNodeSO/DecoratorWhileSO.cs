using System;
using BehaviorTree.Nodes.Decorator;
using UnityEngine;

namespace BehaviorTree.SO.Decorator
{
    [CreateAssetMenu(menuName = "BehaviorTree/Decorator/WhileSO", fileName = "new D_While_Spe")]
    public class DecoratorWhileSO : DecoratorSO
    {
        public BehaviourTreeEnums.NodeState WhileStateCondition;

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