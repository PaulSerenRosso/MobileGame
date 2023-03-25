﻿using BehaviorTree.SO.Actions;

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
        
        public override BehaviorTreeEnums.NodeState Evaluate()
        {
            return BehaviorTreeEnums.NodeState.SUCCESS;
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}