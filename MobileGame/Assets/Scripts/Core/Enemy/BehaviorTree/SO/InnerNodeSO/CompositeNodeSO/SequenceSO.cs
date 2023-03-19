using System;
using BehaviorTree.Nodes.Composite;
using UnityEngine;

namespace BehaviorTree.SO.Composite
{
    [CreateAssetMenu(menuName = "BehaviorTree/Composite/SequenceSO", fileName = "new CO_Sequence_Spe")]
    public class SequenceSO : CompositeSO
    {
        public override Type GetTypeNode()
        {
            return typeof(SequenceNode);
        }

        public override void UpdateComment()
        {
            Comment =
                "Sélectionne un nœud et vérifie si le nœud est un succès ou est en train de fonctionner et retourne le statut, sinon dés qu'un nœud retourne un échec, la sequence retourne un échec";
        }
    }
}