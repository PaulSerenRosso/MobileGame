using BehaviorTree.SO.Actions;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskGetPlayerDirectionNode : ActionNode
    {
        private TaskGetPlayerDirectionNodeSO _so;
        private TaskGetPlayerDirectionNodeDataSO _data;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskGetPlayerDirectionNodeSO)nodeSO;
            _data = (TaskGetPlayerDirectionNodeDataSO)_so.Data;
        }

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            throw new System.NotImplementedException();
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}