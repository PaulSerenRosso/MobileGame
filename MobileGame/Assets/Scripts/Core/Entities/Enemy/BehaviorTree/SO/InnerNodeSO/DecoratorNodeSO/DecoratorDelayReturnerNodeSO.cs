using System;
using BehaviorTree.Nodes.Decorator;
using UnityEngine;

namespace BehaviorTree.SO.Decorator
{
    [CreateAssetMenu(menuName = "BehaviorTree/Decorator/DelayReturnerSO",
        fileName = "new Tree_D_DelayReturner_Spe")]
    public class DecoratorDelayReturnerNodeSO : DecoratorSO
    {
        public BehaviorTreeEnums.NodeState ReturnState;
        public int TimeBeforeToReadChild;

        public override Type GetTypeNode()
        {
            return typeof(DecoratorDelayReturnerNode);
        }

        public override void UpdateComment()
        {
            Comment =
                "Retourne toujours la même valeur qui est renseigné sur le nœud & déclenche les noeuds enfants au bout d'un certain temps";
        }
    }
}