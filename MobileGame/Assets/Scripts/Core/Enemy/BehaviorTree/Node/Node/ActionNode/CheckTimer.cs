using BehaviorTree.SO.Actions;
using HelperPSR.Tick;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class CheckTimer : ActionNode
    {
        private CheckTimerSO _timerSo;
        private CheckTimerDataSO _dataSO;
        private float _timer;

        private TickTimer _tickTimer;

        public override NodeSO GetNodeSO()
        {
            return _timerSo;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _timerSo = (CheckTimerSO)nodeSO;
            _dataSO = (CheckTimerDataSO)_timerSo.Data;
            _timer = _dataSO.StartTime;
        }

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            if (_timer > _dataSO.Time)
            {
                _timer = 0;
                Debug.Log($"Timer is over");
                return BehaviourTreeEnums.NodeState.SUCCESS;
            }
            _timer += Time.deltaTime;
            return BehaviourTreeEnums.NodeState.FAILURE;
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _dataSO;
        }
    }
}