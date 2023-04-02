using System.Collections.Generic;
using BehaviorTree.SO.Composite;
using HelperPSR.Randoms;

namespace BehaviorTree.Nodes.Composite
{
    public class RandomSequenceNode : CompositeNode
    {
        private RandomSequenceSO _so;
        private List<int> _currentChildrenToEvaluate = new();
        private List<int> _currentChildrenProbabilities = new();
        private int _childrenEvaluatedCount;
        private int _pickedChildIndex;
        private int _counter;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (RandomSequenceSO)nodeSO;
        }

        public override void EvaluateChild()
        {
            var node = Children[_currentChildrenToEvaluate[_pickedChildIndex]];
            switch (node.State)
            {
                case BehaviorTreeEnums.NodeState.FAILURE:
                    State = BehaviorTreeEnums.NodeState.FAILURE;
                    ReturnedEvent.Invoke();
                    break;
                case BehaviorTreeEnums.NodeState.SUCCESS:
                    if (_counter < Children.Count - 1)
                    {
                        _counter++;
                        _currentChildrenProbabilities.RemoveAt(_pickedChildIndex);
                        _currentChildrenToEvaluate.RemoveAt(_pickedChildIndex);
                        _pickedChildIndex =
                            RandomHelper.PickRandomElementIndex(_currentChildrenProbabilities.ToArray());
                        Children[_currentChildrenToEvaluate[_pickedChildIndex]].Evaluate();
                    }
                    else
                    {
                        State = BehaviorTreeEnums.NodeState.SUCCESS;
                        ReturnedEvent.Invoke();
                    }
                    break;
            }
        }

        public override void Evaluate()
        {
            _counter = 0;
            InitRandomSelection();
            Children[_currentChildrenToEvaluate[_pickedChildIndex]].Evaluate();
        }

        private void InitRandomSelection()
        {
            _currentChildrenToEvaluate = new List<int>();
            for (int i = 0; i < Children.Count; i++)
            {
                _currentChildrenToEvaluate.Add(i);
            }
            _currentChildrenProbabilities = new List<int>(_so.ChildrenProbabilities);
            _pickedChildIndex =
                RandomHelper.PickRandomElementIndex(_currentChildrenProbabilities.ToArray());
        }
    }
}