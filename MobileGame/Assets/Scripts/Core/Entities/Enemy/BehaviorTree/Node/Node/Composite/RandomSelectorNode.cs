using System.Collections.Generic;
using BehaviorTree.SO.Composite;
using HelperPSR.Randoms;
using UnityEngine;

namespace BehaviorTree.Nodes.Composite
{
    public class RandomSelectorNode : CompositeNode
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

        public override BehaviorTreeEnums.NodeState Evaluate()
        {
            bool isLooping = false;
            _currentChildrenToEvaluate = new List<Node>(Children);
            _childrenEvaluatedCount = 0;
            _currentChildrenProbabilities = new List<int>(_so.ChildrenProbabilities);
            _pickedChildIndex = -1;
            while (_childrenEvaluatedCount < Children.Count)
            {
                if (!isLooping) _pickedChildIndex =
                    RandomHelper.PickRandomElementIndex(_currentChildrenProbabilities.ToArray());
                var currentElement = _currentChildrenToEvaluate[_pickedChildIndex];
                switch (currentElement.Evaluate())
                {
                    case BehaviorTreeEnums.NodeState.FAILURE:
                        isLooping = false;
                        break;
                    case BehaviorTreeEnums.NodeState.SUCCESS:
                        _state = BehaviorTreeEnums.NodeState.SUCCESS;
                        return _state;
                    case BehaviorTreeEnums.NodeState.RUNNING:
                        _state = BehaviorTreeEnums.NodeState.RUNNING;
                        return _state;
                    case BehaviorTreeEnums.NodeState.LOOP:
                        isLooping = true;
                        continue;
                }

                _childrenEvaluatedCount++;
                _currentChildrenProbabilities.RemoveAt(_pickedChildIndex);
                _currentChildrenToEvaluate.RemoveAt(_pickedChildIndex);
            }

            return BehaviorTreeEnums.NodeState.FAILURE;
        }
    }
}