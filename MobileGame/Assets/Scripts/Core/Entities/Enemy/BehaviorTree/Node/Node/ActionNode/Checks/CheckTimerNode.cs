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
            Tree.ResetNodeList.Add(this);
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
            base.Evaluate();
            if (_data.IsInternTimer)
            {
                if (_timer > (float)Sharer.InternValues[_so.InternValues[1].HashCode])
                {
                    ResetTimer();
                    // Debug.Log("timer success " + GetNodeSO().name);
                    State = BehaviorTreeEnums.NodeState.SUCCESS;
                    ReturnedEvent?.Invoke();
                    return;
                }   
            }
            else
            {
                if (_timer > _data.Time)
                {
                    ResetTimer();
                    // Debug.Log("timer success " + GetNodeSO().name);
                    State = BehaviorTreeEnums.NodeState.SUCCESS;
                    ReturnedEvent?.Invoke();
                    return;
                }   
            }

          
            IncreaseTimerEvent?.Invoke();
            _timer += Time.deltaTime;
            // Debug.Log("timer failure " + GetNodeSO().name + _timer + "  " + _data.Time);
            State = BehaviorTreeEnums.NodeState.FAILURE;
            ReturnedEvent?.Invoke();
            // if (ReturnedEvent == null)
            //     Debug.Log("returnevent null" + (TempReturnedEvent == null));
            // else Debug.Log("return event is not null");
        }

        public override void Reset()
        {
            base.Reset();
            ResetTimer();
            _timer = _data.StartTime;
            if (_data.IsSendResetTimerFunction)
            {
                _ResetActionEvent = ResetTimer;
                IncreaseTimerEvent = AddResetTimerFunction;
                EndTimerEvent = RemoveResetTimerEvent;
            }
        }

        private void RemoveResetTimerEvent()
        {
            Sharer.InternValues[_so.InternValues[0].HashCode] = null;
            IncreaseTimerEvent = AddResetTimerFunction;
        }

        private void AddResetTimerFunction()
        {
            Sharer.InternValues[_so.InternValues[0].HashCode] = _ResetActionEvent;
            IncreaseTimerEvent = null;
        }

        private void ResetTimer()
        {
            _timer = 0;
            EndTimerEvent?.Invoke();
//            Debug.Log("reset timer");
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}