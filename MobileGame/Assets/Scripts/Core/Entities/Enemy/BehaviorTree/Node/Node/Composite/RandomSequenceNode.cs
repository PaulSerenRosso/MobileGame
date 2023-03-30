using System.Collections;
using System.Collections.Generic;
using BehaviorTree.SO.Composite;
using HelperPSR.Randoms;
using UnityEngine;

namespace BehaviorTree.Nodes.Composite
{
    public class RandomSequenceNode : CompositeNode
    {
        private RandomSequenceSO _so;
        private List<int> _currentChildrenProbabilities = new();
        private int _childrenEvaluatedCount;
        private int _pickedChildIndex;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (RandomSequenceSO)nodeSO;
        }

        public override IEnumerator Evaluate()
        {
            bool anyChildIsRunning = false;
            int pickRandomElementIndex = 0;
            _childrenEvaluatedCount = 0;
            _currentChildrenProbabilities = new List<int>(_so.ChildrenProbabilities);
            _pickedChildIndex = -1;
            while (_childrenEvaluatedCount < Children.Count)
            {
                pickRandomElementIndex =
                        RandomHelper.PickRandomElementIndex(_currentChildrenProbabilities.ToArray());
                var currentElement = Children[pickRandomElementIndex];
                bool isBlocked = false;
                do
                {
                    CoroutineLauncher.StartCoroutine(currentElement.Evaluate());
                    switch (currentElement.State)
                    {
                        case BehaviorTreeEnums.NodeState.FAILURE:
                            State = BehaviorTreeEnums.NodeState.FAILURE;
                            yield break;
                        case BehaviorTreeEnums.NodeState.SUCCESS:
                            break;
                        case BehaviorTreeEnums.NodeState.RUNNING:
                            anyChildIsRunning = true;
                            break;
                        case BehaviorTreeEnums.NodeState.BLOCKED :
                            isBlocked = true;
                            yield return 0;
                            break;
                            
                    }
                } while (isBlocked);
                _childrenEvaluatedCount++;
                _currentChildrenProbabilities.RemoveAt(pickRandomElementIndex);
            }
            State = anyChildIsRunning ? BehaviorTreeEnums.NodeState.RUNNING : BehaviorTreeEnums.NodeState.SUCCESS;
        }
    }
}