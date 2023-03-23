using BehaviorTree.SO.Actions;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskLookDirectionNode : ActionNode
    {
        private TaskLookDirectionNodeSO _so;
        private TaskLookDirectionNodeDataSO _data;
        
        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskLookDirectionNodeSO)nodeSO;
            _data = (TaskLookDirectionNodeDataSO)_so.Data;
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