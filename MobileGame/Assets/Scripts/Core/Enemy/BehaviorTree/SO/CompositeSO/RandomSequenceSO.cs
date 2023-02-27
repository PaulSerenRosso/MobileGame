using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTree.Nodes.Composite;
using BehaviorTree.SO.Composite;
using UnityEngine;

namespace  BehaviorTree.SO.Composite
{ 
    [CreateAssetMenu(menuName = "BehaviorTree/RandomSequenceSO", fileName = "new RandomSequenceSO")]
public class RandomSequenceSO : CompositeSO
{ 
    public int[] ChildrenProbabilities;
    public override Type GetTypeNode()
    {
        return typeof(RandomSequence);
    }
}
}
