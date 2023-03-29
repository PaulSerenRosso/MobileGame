using BehaviorTree.SO.Actions;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class CheckInternTimerNode : ActionNode
    {
        private CheckInternTimerNodeSO _so;
        private CheckInternTimerNodeDataSO _data;
        private float _timer;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (CheckInternTimerNodeSO)nodeSO;
            _data = (CheckInternTimerNodeDataSO)_so.Data;
        }

        public override BehaviorTreeEnums.NodeState Evaluate()
        {
            if (_timer > (float)Sharer.InternValues[_so.InternValues[0].HashCode])
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