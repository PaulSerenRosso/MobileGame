using System.Collections.Generic;
using BehaviorTree.SO.Composite;
using HelperPSR.Randoms;

namespace BehaviorTree.Nodes.Composite
{
    public class RandomSequence : CompositeNode
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

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            bool anyChildIsRunning = false;
            _childrenEvaluatedCount = 0;
            _currentChildrenProbabilities = new List<int>(_so.ChildrenProbabilities);
            _pickedChildIndex = -1;
            while (_childrenEvaluatedCount < Children.Count)
            {
                var pickRandomElementIndex =
                    RandomHelper.PickRandomElementIndex(_currentChildrenProbabilities.ToArray());
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
                _childrenEvaluatedCount++;
                _currentChildrenProbabilities.RemoveAt(pickRandomElementIndex);
            }

            _state = anyChildIsRunning ? BehaviourTreeEnums.NodeState.RUNNING : BehaviourTreeEnums.NodeState.SUCCESS;
            return _state;
        }
    }
}