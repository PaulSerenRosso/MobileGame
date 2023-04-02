using BehaviorTree.SO.Actions;
using HelperPSR.Collections;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskSetBoolInternValueNode : ActionNode
    {
        private TaskSetBoolInternValueNodeSO _so;
        private TaskSetBoolInternValueNodeDataSO _data;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskSetBoolInternValueNodeSO)nodeSO;
            _data = (TaskSetBoolInternValueNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[0].HashCode, _data.BooleanValue);
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}