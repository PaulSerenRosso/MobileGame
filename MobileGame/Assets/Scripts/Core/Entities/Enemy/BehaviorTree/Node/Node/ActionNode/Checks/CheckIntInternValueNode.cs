using BehaviorTree.SO.Actions;

namespace BehaviorTree.Nodes.Actions
{
    public class CheckIntInternValueNode : ActionNode
    {
        private CheckIntInternValueNodeSO _so;
        private CheckIntInternValueNodeDataSO _data;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (CheckIntInternValueNodeSO)nodeSO;
            _data = (CheckIntInternValueNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            State = (int)Sharer.InternValues[_so.InternValues[0].HashCode] == _data.ValueToCheck
                ? BehaviorTreeEnums.NodeState.SUCCESS
                : BehaviorTreeEnums.NodeState.FAILURE;
            ReturnedEvent?.Invoke();
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}