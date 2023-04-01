using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Player;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskDamagePlayerNode : ActionNode
    {
        private TaskDamagePlayerNodeSO _so;
        private TaskDamagePlayerNodeDataSO _data;

        private PlayerHealth _playerHealth;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskDamagePlayerNodeSO)nodeSO;
            _data = (TaskDamagePlayerNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            _playerHealth.TakeDamage(_data.Damage);
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _playerHealth =
                (PlayerHealth)externDependencyValues[BehaviorTreeEnums.TreeExternValues.PlayerHealth];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}