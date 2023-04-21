using BehaviorTree.SO.Actions;
using HelperPSR.Collections;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskRandomRangeNode : ActionNode
    {
        private TaskRandomRangeNodeSO _so;
        private TaskRandomRangeNodeDataSO _data;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskRandomRangeNodeSO)nodeSO;
            _data = (TaskRandomRangeNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            var random = Random.Range(_data.MinValue, _data.MaxValue);
            CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[0].HashCode, random);
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}