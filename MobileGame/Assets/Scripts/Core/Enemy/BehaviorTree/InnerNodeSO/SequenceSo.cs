using System;
using BehaviorTree.Nodes;
using UnityEngine;

namespace BehaviorTree.InnerNode
{
    [CreateAssetMenu(menuName = "BehaviorTree/StructSequenceSO", fileName = "new StructSequenceSO")]
    public class SequenceSo : InnerNodeSO
    {
        public override Type GetTypeNode()
        {
            return typeof(Sequence);
        }
    }
}