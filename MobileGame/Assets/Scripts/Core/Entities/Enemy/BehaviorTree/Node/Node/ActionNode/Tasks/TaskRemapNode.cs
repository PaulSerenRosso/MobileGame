using BehaviorTree.SO.Actions;
using HelperPSR.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskRemapNode : ActionNode
    {
        private TaskRemapNodeSO _so;
        private TaskRemapNodeDataSO _data;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskRemapNodeSO)nodeSO;
            _data = (TaskRemapNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            var value = math.remap(_data.OldMinValue, _data.OldMaxValue, _data.NewMinValue, _data.NewMaxValue,
                (float)Sharer.InternValues[_so.InternValues[0].HashCode]);
            CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[1].HashCode, (int)value);
            Debug.Log($"{_so.InternValues[1].Key} / value {value}" );
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}