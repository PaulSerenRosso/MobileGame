using System;
using BehaviorTree.Nodes.Composite;
using UnityEngine;

namespace BehaviorTree.SO.Composite
{
    [CreateAssetMenu(menuName = "BehaviorTree/Composite/RandomSelectorSO", fileName = "new CO_RandomSelector_Spe")]
    public class RandomSelectorSO : CompositeSO
    {
        public int[] ChildrenProbabilities;

        public override Type GetTypeNode()
        {
            return typeof(RandomSelectorNode);
        }

        public override void UpdateCommentary()
        {
            
        }
    }
}