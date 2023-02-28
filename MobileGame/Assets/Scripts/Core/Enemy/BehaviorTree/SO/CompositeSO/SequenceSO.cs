using System;
using BehaviorTree.Nodes.Composite;
using UnityEngine;

namespace BehaviorTree.SO.Composite
{
    [CreateAssetMenu(menuName = "BehaviorTree/Struct/SequenceSO", fileName = "new SequenceSO")]
    public class SequenceSO : CompositeSO
    {
        public override Type GetTypeNode()
        {
            return typeof(Sequence);
        }
    }
}