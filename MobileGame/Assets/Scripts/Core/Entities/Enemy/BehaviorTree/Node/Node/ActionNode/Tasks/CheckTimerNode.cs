using BehaviorTree.SO.Actions;
using HelperPSR.Tick;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class CheckTimerNode : ActionNode
    {
        private CheckTimerNodeSO _so;
        private CheckTimerNodeDataSO _data;
        private float _timer;

        private TickTimer _tickTimer;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (CheckTimerNodeSO)nodeSO;
            _data = (CheckTimerNodeDataSO)_so.Data;
            _timer = _data.StartTime;
        }

        public override BehaviorTreeEnums.NodeState Evaluate()
        {
            if (_timer > _data.Time)
            {
                _timer = 0;
                return BehaviorTreeEnums.NodeState.SUCCESS;
            }
            _timer += Time.deltaTime;
            return BehaviorTreeEnums.NodeState.FAILURE;
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}