using System;
using BehaviorTree.Nodes;
using UnityEngine;

namespace BehaviorTree.Struct
{
    [CreateAssetMenu(menuName = "BehaviorTree/StructSelectorSO", fileName = "new StructSelectorSO")]
    public class StructSelectorSO : InnerNodeStructSO
    {
        public override Type GetTypeNode()
        {
            return typeof(Selector);
        }
    }
}