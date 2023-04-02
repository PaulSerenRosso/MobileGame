using System;
using BehaviorTree.SO.Actions;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class CheckTimerNode : ActionNode
    {
        private CheckTimerNodeSO _so;
        private CheckTimerNodeDataSO _data;
        private float _timer;
        private Action _ResetActionEvent;
        private event Action IncreaseTimerEvent;
        private event Action EndTimerEvent;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (CheckTimerNodeSO)nodeSO;
            _data = (CheckTimerNodeDataSO)_so.Data;
            _timer = _data.StartTime;
            if (_data.IsSendResetTimerFunction)
            {
                _ResetActionEvent = ResetTimer;
                IncreaseTimerEvent = AddResetTimerFunction;
                EndTimerEvent = RemoveResetTimerEvent;
            }
        }

        public override void Evaluate()
        {
            if (_timer > _data.Time)
            {
                ResetTimer();
                EndTimerEvent?.Invoke();
                State = BehaviorTreeEnums.NodeState.SUCCESS;
                ReturnedEvent?.Invoke();
                return;
            }

            IncreaseTimerEvent?.Invoke();
            _timer += Time.deltaTime;
            State = BehaviorTreeEnums.NodeState.FAILURE;
            ReturnedEvent?.Invoke();
        }

        private void RemoveResetTimerEvent()
        {
      
            Sharer.InternValues[0] = null;
            IncreaseTimerEvent = AddResetTimerFunction;
        }

        private void AddResetTimerFunction()
        {
           
            Sharer.InternValues[0] = _ResetActionEvent;
            IncreaseTimerEvent = null;
        }

        private void ResetTimer()
        {
         
            _timer = 0;
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}