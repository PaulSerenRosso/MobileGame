using System.Collections.Generic;
using BehaviorTree.SO.Actions;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskSetBoostNode: ActionNode
    {
        private TaskSetBoostNodeSO _so;
        private TaskSetBoostNodeDataSO _data;
        private EnemyManager _enemyManager;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskSetBoostNodeSO)nodeSO;
            _data = (TaskSetBoostNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            _enemyManager.IsBoosted = _data.BooleanValue;
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues, Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _enemyManager = (EnemyManager)enemyDependencyValues[BehaviorTreeEnums.TreeEnemyValues.EnemyManager];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}