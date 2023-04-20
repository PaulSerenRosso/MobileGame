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
            int value = (int)Sharer.InternValues[_so.InternValues[0].HashCode];
            switch (_data.InternValueIntCalculate)
            {
                case BehaviorTreeEnums.InternValueIntCalculate.ADD:
                    value += _data.Value;
                    break;
                case BehaviorTreeEnums.InternValueIntCalculate.SUBTRACT:
                    value -= _data.Value;
                    break;
                case BehaviorTreeEnums.InternValueIntCalculate.SET:
                    value = _data.Value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[1].HashCode, value);
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}