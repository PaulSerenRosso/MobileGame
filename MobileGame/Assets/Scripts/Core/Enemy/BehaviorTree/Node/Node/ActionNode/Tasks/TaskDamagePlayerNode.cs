using BehaviorTree.SO.Actions;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskDamagePlayerNode : ActionNode
    {
        private TaskDamagePlayerNodeSO _so;
        private TaskDamagePlayerNodeDataSO _data;
        
        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskDamagePlayerNodeSO)nodeSO;
            _data = (TaskDamagePlayerNodeDataSO)_so.Data;
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