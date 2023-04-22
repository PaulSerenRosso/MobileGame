using System;
using BehaviorTree.Nodes.Decorator;
using UnityEngine;

namespace BehaviorTree.SO.Decorator
{
    [CreateAssetMenu(menuName = "BehaviorTree/Decorator/ReturnerSO",
        fileName = "new Tree_D_Returner_Spe")]
    public class DecoratorReturnerSO : DecoratorSO
    {
        public BehaviorTreeEnums.NodeState ReturnState;

        public override Type GetTypeNode()
        {
            return typeof(DecoratorReturnerNode);
        }

        public override void UpdateComment()
        {
            Comment = "Retourne toujours la même valeur qui est renseigné sur le nœud";
        }
    }
}