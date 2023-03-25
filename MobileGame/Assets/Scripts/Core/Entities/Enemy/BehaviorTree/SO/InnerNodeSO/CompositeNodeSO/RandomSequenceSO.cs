using System;
using BehaviorTree.Nodes.Composite;
using UnityEngine;

namespace BehaviorTree.SO.Composite
{
    [CreateAssetMenu(menuName = "BehaviorTree/Composite/RandomSequenceSO", fileName = "new CO_RandomSequence_Spe")]
    public class RandomSequenceSO : CompositeSO
    {
        public int[] ChildrenProbabilities;

        public override Type GetTypeNode()
        {
            return typeof(RandomSequenceNode);
        }

        public override void UpdateComment()
        {
            Comment =
                "Sélectionne un nœud aléatoirement et vérifie si le nœud est un succès ou est en train de fonctionner et retourne le statut, sinon dés qu'un nœud retourne un échec, la sequence retourne un échec";
        }
    }
}