using System.Collections.Generic;
using BehaviorTree.SO.Composite;
using HelperPSR.Randoms;

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

        public override BehaviorTreeEnums.NodeState Evaluate()
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
                    case BehaviorTreeEnums.NodeState.FAILURE:
                        _state = BehaviorTreeEnums.NodeState.FAILURE;
                        return _state;
                    case BehaviorTreeEnums.NodeState.SUCCESS:
                        break;
                    case BehaviorTreeEnums.NodeState.RUNNING:
                        anyChildIsRunning = true;
                        break;
                }
                _childrenEvaluatedCount++;
                _currentChildrenProbabilities.RemoveAt(pickRandomElementIndex);
            }

            _state = anyChildIsRunning ? BehaviorTreeEnums.NodeState.RUNNING : BehaviorTreeEnums.NodeState.SUCCESS;
            return _state;
        }
    }
}