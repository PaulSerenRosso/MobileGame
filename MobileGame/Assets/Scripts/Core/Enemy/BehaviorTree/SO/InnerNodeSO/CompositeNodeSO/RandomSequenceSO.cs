using System;
using BehaviorTree.Nodes.Composite;
using UnityEngine;

namespace BehaviorTree.SO.Composite
{
    [CreateAssetMenu(menuName = "BehaviorTree/Composite/RandomSequenceSO", fileName = "new CO_RandomSequence_Spe")]
    public class RandomSequenceSO : CompositeSO
    {
        public int[] ChildrenProbabilities;

        public override Type GetTypeNode()
        {
            return typeof(RandomSequenceNode);
        }

        public override void UpdateCommentary()
        {
            
        }
    }
}