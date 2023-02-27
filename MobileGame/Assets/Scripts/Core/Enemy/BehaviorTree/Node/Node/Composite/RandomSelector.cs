using System.Collections;
using System.Collections.Generic;
using BehaviorTree.SO.Composite;
using HelperPSR.Randoms;
using UnityEngine;

namespace BehaviorTree.Nodes.Composite
{
public class RandomSelector : CompositeNode
{
    private RandomSelectorSO so;
    private List<int> currentChildrenProbabilities = new List<int>();
    private int childrenEvaluatedCount;
    private int pickedChildIndex;
    public override NodeSO GetNodeSO()
    {
        return so;
    }

    public override void SetNodeSO(NodeSO nodeSO)
    {
        so =(RandomSelectorSO) nodeSO;
    }

    public override BehaviourTreeEnums.NodeState Evaluate()
    {
        childrenEvaluatedCount = 0;
        currentChildrenProbabilities = new List<int>(so.ChildrenProbabilities);
        pickedChildIndex = -1;
        while (childrenEvaluatedCount < Children.Count)
        {
        var pickRandomElementIndex = RandomHelper.PickRandomElementIndex(currentChildrenProbabilities.ToArray());
            var currentElement = Children[pickRandomElementIndex];
            switch (currentElement.Evaluate())
            {
                case BehaviourTreeEnums.NodeState.FAILURE:
                {
                    break;
                }
                case BehaviourTreeEnums.NodeState.SUCCESS:
                    _state = BehaviourTreeEnums.NodeState.SUCCESS;
                    return _state;
                case BehaviourTreeEnums.NodeState.RUNNING:
                    _state = BehaviourTreeEnums.NodeState.RUNNING;
                    return _state;
            }
            childrenEvaluatedCount++;
            currentChildrenProbabilities.RemoveAt(pickRandomElementIndex);
        }
        return BehaviourTreeEnums.NodeState.FAILURE;
    }
    
}
}
