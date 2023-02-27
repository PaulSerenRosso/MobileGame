using System.Collections.Generic;
using BehaviorTree.SO.Composite;
using HelperPSR.Randoms;
using UnityEngine;

namespace BehaviorTree.Nodes.Composite
{
    public class RandomSelector : CompositeNode
    {
        private RandomSelectorSO _so;
        private List<int> _currentChildrenProbabilities = new List<int>();
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

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            _currentChildrenToEvaluate = new List<Node>(Children);
            _childrenEvaluatedCount = 0;
            _currentChildrenProbabilities = new List<int>(_so.ChildrenProbabilities);
            _pickedChildIndex = -1;
            while (_childrenEvaluatedCount < Children.Count)
            {
                _pickedChildIndex =
                    RandomHelper.PickRandomElementIndex(_currentChildrenProbabilities.ToArray());
                Debug.Log($"picked: {_pickedChildIndex}");
                Debug.Log($"count proba: {_currentChildrenProbabilities.Count}");
                Debug.Log($"count children evaluate: {_currentChildrenToEvaluate.Count}");
                var currentElement = _currentChildrenToEvaluate[_pickedChildIndex];
                switch (currentElement.Evaluate())
                {
                    case BehaviourTreeEnums.NodeState.FAILURE:
                        break;
                    case BehaviourTreeEnums.NodeState.SUCCESS:
                        _state = BehaviourTreeEnums.NodeState.SUCCESS;
                        return _state;
                    case BehaviourTreeEnums.NodeState.RUNNING:
                        _state = BehaviourTreeEnums.NodeState.RUNNING;
                        return _state;
                }

                _childrenEvaluatedCount++;
                _currentChildrenProbabilities.RemoveAt(_pickedChildIndex);
                _currentChildrenToEvaluate.RemoveAt(_pickedChildIndex);
            }

            return BehaviourTreeEnums.NodeState.FAILURE;
        }
    }
}