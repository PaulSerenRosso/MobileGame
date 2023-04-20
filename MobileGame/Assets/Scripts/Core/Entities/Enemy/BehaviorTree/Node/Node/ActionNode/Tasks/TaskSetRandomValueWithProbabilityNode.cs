using System;
using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using HelperPSR.Collections;
using HelperPSR.Randoms;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskSetRandomValueWithProbabilityNode : ActionNode
    {
        private TaskSetRandomValueWithProbabilityNodeSO _so;
        private TaskSetRandomValueWithProbabilityNodeDataSO _data;
        private List<int> _currentProbabilities = new();
        private List<int> _randomProbilityIndexes = new();

        private Action _resetRandomSelectionEvent;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            Tree.ResetNodeList.Add(this);
            _so = (TaskSetRandomValueWithProbabilityNodeSO)nodeSO;
            _data = (TaskSetRandomValueWithProbabilityNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            if (_randomProbilityIndexes.Count == 0)
            {
                State = BehaviorTreeEnums.NodeState.FAILURE;
            }
            else
            {
                var randomPick = RandomHelper.PickRandomElementIndex(_currentProbabilities.ToArray());
                CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[^2].HashCode,
                    _randomProbilityIndexes[randomPick]);
                _randomProbilityIndexes.RemoveAt(randomPick);
                _currentProbabilities.RemoveAt(randomPick);
                State = BehaviorTreeEnums.NodeState.SUCCESS;
            }
            ReturnedEvent?.Invoke();
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }

        public override void SetDependencyValues(Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues, Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            SetRandomSelection();
        }

        public override void Reset()
        {
            base.Reset();
            SetRandomSelection();
        }

        private void SetRandomSelection()
        {
            for (var index = 0; index < _data.StartProbabilitiesValues.Length; index++)
            {
                var probabilityValue = _data.StartProbabilitiesValues[index];
                CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[index].HashCode, probabilityValue);
            }
            ResetRandomSelection();
            _resetRandomSelectionEvent = ResetRandomSelection;
            CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[^1].HashCode,
                _resetRandomSelectionEvent);
        }

        public void ResetRandomSelection()
        {
            _randomProbilityIndexes.Clear();
            _currentProbabilities.Clear();
            for (int i = 0; i < _so.InternValues.Count - 2; i++)
            {
                _randomProbilityIndexes.Add(i);
                _currentProbabilities.Add((int)Sharer.InternValues[_so.InternValues[i].HashCode]);
            }
        }
    }
}