using System.Collections;
using System.Collections.Generic;
using BehaviorTree.Nodes;
using BehaviorTree.SO.Composite;
using HelperPSR.Randoms;
using UnityEngine;

namespace BehaviorTree.Nodes.Composite
{
    public class RandomSequence : CompositeNode
    {
        private RandomSequenceSO so;
        private List<int> currentChildrenProbabilities = new List<int>();
        private int childrenEvaluatedCount;
        private int pickedChildIndex;

        public override NodeSO GetNodeSO()
        {
            return so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            so = (RandomSequenceSO)nodeSO;
        }

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            bool anyChildIsRunning = false;
            childrenEvaluatedCount = 0;
            currentChildrenProbabilities = new List<int>(so.ChildrenProbabilities);
            pickedChildIndex = -1;
            while (childrenEvaluatedCount < Children.Count)
            {
                var pickRandomElementIndex =
                    RandomHelper.PickRandomElementIndex(currentChildrenProbabilities.ToArray());
                var currentElement = Children[pickRandomElementIndex];
                switch (currentElement.Evaluate())
                {
                    case BehaviourTreeEnums.NodeState.FAILURE:
                        _state = BehaviourTreeEnums.NodeState.FAILURE;
                        return _state;
                    case BehaviourTreeEnums.NodeState.SUCCESS:
                        break;
                    case BehaviourTreeEnums.NodeState.RUNNING:
                        anyChildIsRunning = true;
                        break;
                }
                childrenEvaluatedCount++;
                currentChildrenProbabilities.RemoveAt(pickRandomElementIndex);
            }

            _state = anyChildIsRunning ? BehaviourTreeEnums.NodeState.RUNNING : BehaviourTreeEnums.NodeState.SUCCESS;
            return _state;
        }
    }
}