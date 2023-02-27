using System;
using BehaviorTree.Nodes;
using BehaviorTree.Nodes.Composite;
using UnityEngine;

namespace BehaviorTree.SO.Composite
{
    [CreateAssetMenu(menuName = "BehaviorTree/StructSelectorSO", fileName = "new StructSelectorSO")]
    public class SelectorSO : CompositeSO
    {
        public override Type GetTypeNode()
        {
            return typeof(Selector);
        }
    }
}