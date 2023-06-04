using BehaviorTree.SO.Actions;

namespace BehaviorTree.Nodes.Actions
{
    public class CheckBoolInternValueNode : ActionNode
    {
        private CheckBoolInternValueNodeSO _so;
        private CheckBoolInternValueNodeDataSO _data;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (CheckBoolInternValueNodeSO)nodeSO;
            _data = (CheckBoolInternValueNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            if (!Sharer.InternValues.ContainsKey(_so.InternValues[0].HashCode))
            {
                State = BehaviorTreeEnums.NodeState.FAILURE;
                ReturnedEvent?.Invoke();
                return;
            }

            State = (bool)Sharer.InternValues[_so.InternValues[0].HashCode] == _data.ValueToCompare
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