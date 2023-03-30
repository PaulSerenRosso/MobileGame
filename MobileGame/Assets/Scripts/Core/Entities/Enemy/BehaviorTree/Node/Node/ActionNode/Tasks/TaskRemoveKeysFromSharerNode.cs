using System.Collections;
using System.Collections.Generic;
using BehaviorTree.SO.Actions;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskRemoveKeysFromSharerNode : ActionNode
    {
        private TaskRemoveKeysFromSharerNodeSO _so;
        private TaskRemoveKeysFromSharerNodeDataSO _data;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskRemoveKeysFromSharerNodeSO)nodeSO;
            _data = (TaskRemoveKeysFromSharerNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override IEnumerator Evaluate()
        {
            
            foreach (var key in _so.InternValues)
            {
                Sharer.InternValues.Remove(key.HashCode);
            }
       State = BehaviorTreeEnums.NodeState.SUCCESS;
       yield break;
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}