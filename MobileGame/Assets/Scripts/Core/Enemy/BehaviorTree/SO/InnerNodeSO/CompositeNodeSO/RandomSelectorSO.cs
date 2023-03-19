using System;
using BehaviorTree.Nodes.Composite;
using UnityEngine;

namespace BehaviorTree.SO.Composite
{
    [CreateAssetMenu(menuName = "BehaviorTree/Composite/RandomSelectorSO", fileName = "new CO_RandomSelector_Spe")]
    public class RandomSelectorSO : CompositeSO
    {
        public int[] ChildrenProbabilities;

        public override Type GetTypeNode()
        {
            return typeof(RandomSelectorNode);
        }

        public override void UpdateComment()
        {
            Comment =
                "Sélectionne un nœud aléatoirement, vérifie si le nœud est un succès ou est en train de fonctionner et retourne le statut, sinon il continue de parcourir les nœuds jusqu'à un succés ou un fonctionnement";
        }
    }
}