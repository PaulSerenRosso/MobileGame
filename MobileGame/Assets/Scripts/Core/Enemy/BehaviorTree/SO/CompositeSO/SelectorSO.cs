using System;
using BehaviorTree.Nodes.Composite;
using UnityEngine;

namespace BehaviorTree.SO.Composite
{
    [CreateAssetMenu(menuName = "BehaviorTree/Struct/SelectorSO", fileName = "new SelectorSO")]
    public class SelectorSO : CompositeSO
    {
        public override Type GetTypeNode()
        {
            return typeof(Selector);
        }
    }
}