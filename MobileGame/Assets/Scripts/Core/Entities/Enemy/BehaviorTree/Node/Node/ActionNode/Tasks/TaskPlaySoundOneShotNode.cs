using System.Collections.Generic;
using BehaviorTree.SO.Actions;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskPlaySoundOneShotNode : ActionNode
    {
        private TaskPlaySoundOneShotNodeSO _so;
        private TaskPlaySoundOneShotNodeDataSO _data;
        
        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskPlaySoundOneShotNodeSO)nodeSO;
            _data = (TaskPlaySoundOneShotNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }
        
        public override IEnumerator<BehaviorTreeEnums.NodeState> Evaluate()
        {
            yield return BehaviorTreeEnums.NodeState.SUCCESS;
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}