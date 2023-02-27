using System;
using BehaviorTree.Nodes;
using BehaviorTree.Nodes.Composite;
using UnityEngine;

namespace BehaviorTree.SO.Composite
{
    [CreateAssetMenu(menuName = "BehaviorTree/StructSequenceSO", fileName = "new StructSequenceSO")]
    public class SequenceSO : CompositeSO
    {
        public override Type GetTypeNode()
        {
            return typeof(Sequence);
        }
    }
}