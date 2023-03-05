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

        public override void UpdateCommentary()
        {
            
        }
    }
}