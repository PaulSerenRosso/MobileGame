using System.Collections;
using System.Collections.Generic;
using BehaviorTree.SO.Actions;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskShaderSetFloatNode : ActionNode
    {
        private TaskShaderSetFloatNodeSO _so;
        private TaskShaderSetFloatNodeDataSO _data;
        
        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskShaderSetFloatNodeSO)nodeSO;
            _data = (TaskShaderSetFloatNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }
        
        public override IEnumerator Evaluate()
        {
           State = BehaviorTreeEnums.NodeState.SUCCESS;
            yield break;
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}