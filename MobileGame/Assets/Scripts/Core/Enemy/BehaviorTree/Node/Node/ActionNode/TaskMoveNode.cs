using BehaviorTree.SO.Actions;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskMoveNode : ActionNode
    {
        private TaskEnemyMoveNodeSO _taskEnemyMoveNodeSo;
        private TaskMoveNodeDataSO _taskMoveNodeDataSO;
        
        public override NodeSO GetNodeSO()
        {
            return _taskEnemyMoveNodeSo;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _taskEnemyMoveNodeSo = (TaskEnemyMoveNodeSO)nodeSO;
            _taskMoveNodeDataSO = (TaskMoveNodeDataSO)_taskEnemyMoveNodeSo.Data;
        }

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _taskMoveNodeDataSO;
        }
    }
}