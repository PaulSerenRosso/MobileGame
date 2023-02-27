using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTree.Nodes.Composite;
using UnityEngine;

namespace BehaviorTree.SO.Composite
{
    [CreateAssetMenu(menuName = "BehaviorTree/RandomSelectorSO", fileName = "new RandomSelectorSO")]
    public class RandomSelectorSO : CompositeSO
{
    public int[] ChildrenProbabilities;
    public override Type GetTypeNode()
    {
        return typeof(RandomSelector);
    }
}
}
