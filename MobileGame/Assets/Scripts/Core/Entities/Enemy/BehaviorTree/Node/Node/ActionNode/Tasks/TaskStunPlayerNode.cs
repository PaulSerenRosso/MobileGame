using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Player;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskStunPlayerNode : ActionNode
    {
        private TaskStunPlayerNodeSO _so;
        private TaskStunPlayerNodeDataSO _data;
        private PlayerController _playerController;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskStunPlayerNodeSO)nodeSO;
            _data = (TaskStunPlayerNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            _playerController.TakeStun();
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _playerController = (PlayerController)externDependencyValues[BehaviorTreeEnums.TreeExternValues.PlayerController];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}