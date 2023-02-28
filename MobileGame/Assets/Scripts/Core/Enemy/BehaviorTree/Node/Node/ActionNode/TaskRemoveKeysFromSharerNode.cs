using BehaviorTree.SO.Actions;
using Core.Enemy.BehaviorTree.SO.ActionsSO;

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

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            foreach (var key in _so.KeysToRemove)
            {
                Sharer.InternValues.Remove(key.HashCode);
            }
            return BehaviourTreeEnums.NodeState.SUCCESS;
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}