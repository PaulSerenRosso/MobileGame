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

        public override IEnumerator<BehaviorTreeEnums.NodeState> Evaluate()
        {
            _currentChildrenToEvaluate = new List<Node>(Children);
            _childrenEvaluatedCount = 0;
            _currentChildrenProbabilities = new List<int>(_so.ChildrenProbabilities);
            _pickedChildIndex = -1;
            while (_childrenEvaluatedCount < Children.Count)
            {
                _pickedChildIndex =
                    RandomHelper.PickRandomElementIndex(_currentChildrenProbabilities.ToArray());
                var currentElement = _currentChildrenToEvaluate[_pickedChildIndex];
                IEnumerator<BehaviorTreeEnums.NodeState> state = currentElement.Evaluate();
                state.MoveNext();
                switch (state.Current)
                {
                    case BehaviorTreeEnums.NodeState.FAILURE:
                        break;
                    case BehaviorTreeEnums.NodeState.SUCCESS:
                        _state = BehaviorTreeEnums.NodeState.SUCCESS;
                        yield return _state;
                        yield break;
                    case BehaviorTreeEnums.NodeState.RUNNING:
                        _state = BehaviorTreeEnums.NodeState.RUNNING;
                        yield return _state;
                        yield break;
                }

                _childrenEvaluatedCount++;
                _currentChildrenProbabilities.RemoveAt(_pickedChildIndex);
                _currentChildrenToEvaluate.RemoveAt(_pickedChildIndex);
            }

            yield return BehaviorTreeEnums.NodeState.FAILURE;
        }
    }
}