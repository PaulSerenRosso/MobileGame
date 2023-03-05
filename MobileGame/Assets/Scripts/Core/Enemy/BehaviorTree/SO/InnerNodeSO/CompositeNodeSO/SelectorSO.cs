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

        public override void UpdateCommentary()
        {
            
        }
    }
}