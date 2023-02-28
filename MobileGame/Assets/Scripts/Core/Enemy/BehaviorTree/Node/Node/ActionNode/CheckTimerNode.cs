using BehaviorTree.SO.Actions;
using HelperPSR.Tick;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class CheckTimerNode : ActionNode
    {
        private CheckTimerNodeSO _timerNodeSO;
        private CheckTimerNodeDataSO _nodeDataSO;
        private float _timer;

        private TickTimer _tickTimer;

        public override NodeSO GetNodeSO()
        {
            return _timerNodeSO;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _timerNodeSO = (CheckTimerNodeSO)nodeSO;
            _nodeDataSO = (CheckTimerNodeDataSO)_timerNodeSO.Data;
            _timer = _nodeDataSO.StartTime;
        }

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            if (_timer > _nodeDataSO.Time)
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
            return _nodeDataSO;
        }
    }
}