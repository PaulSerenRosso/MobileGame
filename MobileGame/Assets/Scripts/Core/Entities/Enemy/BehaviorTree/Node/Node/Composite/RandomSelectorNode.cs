using System.Collections;
using System.Collections.Generic;
using BehaviorTree.SO.Composite;
using HelperPSR.Randoms;
using UnityEngine;

namespace BehaviorTree.Nodes.Composite
{
    public class RandomSelectorNode : CompositeNode
    {
        private RandomSelectorSO _so;
        private List<int> _currentChildrenProbabilities = new();
        private int _childrenEvaluatedCount;
        private int _pickedChildIndex;
        private List<Node> _currentChildrenToEvaluate;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (RandomSelectorSO)nodeSO;
        }

        public override IEnumerator Evaluate()
        {
            _currentChildrenToEvaluate = new List<Node>(Children);
            _childrenEvaluatedCount = 0;
            _currentChildrenProbabilities = new List<int>(_so.ChildrenProbabilities);
            _pickedChildIndex = -1;
            while (_childrenEvaluatedCount < Children.Count)
            {
                _pickedChildIndex =
                    RandomHelper.PickRandomElementIndex(_currentChildrenProbabilities.ToArray());
                bool isBlocked = false;
                do
                {
                    var currentElement = _currentChildrenToEvaluate[_pickedChildIndex];
                  CoroutineLauncher.StartCoroutine(currentElement.Evaluate());
                    switch (currentElement.State)
                    {
                        case BehaviorTreeEnums.NodeState.FAILURE:
                            break;
                        case BehaviorTreeEnums.NodeState.SUCCESS:
                            State = BehaviorTreeEnums.NodeState.SUCCESS;
                            yield break;
                        case BehaviorTreeEnums.NodeState.RUNNING:
                            State = BehaviorTreeEnums.NodeState.RUNNING;
                            yield break;
                        case BehaviorTreeEnums.NodeState.BLOCKED:
                            isBlocked = true;
                            yield return 0;
                            break;
                    }
                } while (isBlocked);
                _childrenEvaluatedCount++;
                _currentChildrenProbabilities.RemoveAt(_pickedChildIndex);
                _currentChildrenToEvaluate.RemoveAt(_pickedChildIndex);
            }
            State = BehaviorTreeEnums.NodeState.FAILURE;
        }

    }

}