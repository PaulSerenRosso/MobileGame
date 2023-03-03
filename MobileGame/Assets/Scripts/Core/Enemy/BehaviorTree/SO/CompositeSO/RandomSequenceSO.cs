using System;
using BehaviorTree.Nodes.Composite;
using UnityEngine;

namespace BehaviorTree.SO.Composite
{
    [CreateAssetMenu(menuName = "BehaviorTree/Struct/RandomSequenceSO", fileName = "new RandomSequenceSO")]
    public class RandomSequenceSO : CompositeSO
    {
        public int[] ChildrenProbabilities;

        public override Type GetTypeNode()
        {
            return typeof(RandomSequence);
        }

        public override void UpdateCommentary()
        {
            
        }
    }
}