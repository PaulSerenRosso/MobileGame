using System;
using BehaviorTree.Nodes.Composite;
using UnityEngine;

namespace BehaviorTree.SO.Composite
{
    [CreateAssetMenu(menuName = "BehaviorTree/Composite/SelectorSO", fileName = "new CO_Selector_Spe")]
    public class SelectorSO : CompositeSO
    {
        public override Type GetTypeNode()
        {
            return typeof(SelectorNode);
        }

        public override void UpdateComment()
        {
            Comment =
                "Sélectionne un nœud et vérifie si le nœud est un succès ou est en train de fonctionner et retourne le statut, sinon il continue de parcourir les nœuds jusqu'à un succés ou un fonctionnement";
        }
    }
}