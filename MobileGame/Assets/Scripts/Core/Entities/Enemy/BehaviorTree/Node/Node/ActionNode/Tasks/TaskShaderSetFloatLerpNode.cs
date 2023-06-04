using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Player;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskShaderSetFloatLerpNode : ActionNode
    {
        private TaskShaderSetFloatLerpNodeSO _so;
        private TaskShaderSetFloatLerpNodeDataSO _data;
        
        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskShaderSetFloatLerpNodeSO)nodeSO;
            _data = (TaskShaderSetFloatLerpNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }
        
        public override void Evaluate()
        {
            base.Evaluate();
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }
        
        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}