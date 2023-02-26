using System;
using BehaviorTree.Nodes;
using UnityEngine;

namespace BehaviorTree.InnerNode
{
    [CreateAssetMenu(menuName = "BehaviorTree/StructSelectorSO", fileName = "new StructSelectorSO")]
    public class SelectorSo : InnerNodeSO
    {
        public override Type GetTypeNode()
        {
            return typeof(Selector);
        }
    }
}