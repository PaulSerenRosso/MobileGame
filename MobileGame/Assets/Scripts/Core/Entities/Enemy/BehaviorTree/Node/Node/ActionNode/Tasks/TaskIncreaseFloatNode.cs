using System;
using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using HelperPSR.Collections;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskIncreaseFloatNode : ActionNode
    {
        private TaskIncreaseFloatNodeSO _so;
        private TaskIncreaseFloatNodeDataSO _data;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            Tree.ResetNodeList.Add(this);
            _so = (TaskIncreaseFloatNodeSO)nodeSO;
            _data = (TaskIncreaseFloatNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            float result;
            switch (_data.InternValueCalculate)
            {
                case BehaviorTreeEnums.InternValueCalculate.ADD:
                    result = (float)Sharer.InternValues[_so.InternValues[0].HashCode] + (_data.FloatValue * Time.deltaTime);
                    break;
                case BehaviorTreeEnums.InternValueCalculate.SUBTRACT:
                    result = (float)Sharer.InternValues[_so.InternValues[0].HashCode] -
                             (_data.FloatValue * Time.deltaTime);
                    break;
                case BehaviorTreeEnums.InternValueCalculate.SET:
                    result = (float)Sharer.InternValues[_so.InternValues[0].HashCode];
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[0].HashCode, result);
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void Reset()
        {
            CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[0].HashCode, _data.StartValue);
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}