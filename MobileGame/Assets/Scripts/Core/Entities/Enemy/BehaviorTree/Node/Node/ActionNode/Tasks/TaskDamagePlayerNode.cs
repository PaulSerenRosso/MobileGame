using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Player;
using UnityEngine;

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

        public override IEnumerator<BehaviorTreeEnums.NodeState> Evaluate()
        {
            // TODO: think about combo attack of boss
            Debug.Log("Boss Attack");
            _playerHealth.TakeDamage(_data.Damage);
            yield return BehaviorTreeEnums.NodeState.SUCCESS;
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