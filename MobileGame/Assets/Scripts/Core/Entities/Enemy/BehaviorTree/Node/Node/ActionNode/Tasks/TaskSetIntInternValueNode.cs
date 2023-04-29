using System;
using BehaviorTree.SO.Actions;
using HelperPSR.Collections;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskSetIntInternValueNode : ActionNode
    {
        private TaskSetIntInternValueNodeSO _so;
        private TaskSetIntInternValueNodeDataSO _data;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskSetIntInternValueNodeSO)nodeSO;
            _data = (TaskSetIntInternValueNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            float value;
            if (_data.IsValueInternValue)
            {
                value = (float)Sharer.InternValues[_so.InternValues[2].HashCode];
            }
            else
            {
                value = _data.Value;
            }
            int probability = (int)Sharer.InternValues[_so.InternValues[0].HashCode];
            switch (_data.InternValueCalculate)
            {
                case BehaviorTreeEnums.InternValueCalculate.ADD:
                    probability += (int)value;
                    break;
                case BehaviorTreeEnums.InternValueCalculate.SUBTRACT:
                    probability -= (int)value;
                    break;
                case BehaviorTreeEnums.InternValueCalculate.SET:
                    probability = (int)value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[1].HashCode, probability);
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}