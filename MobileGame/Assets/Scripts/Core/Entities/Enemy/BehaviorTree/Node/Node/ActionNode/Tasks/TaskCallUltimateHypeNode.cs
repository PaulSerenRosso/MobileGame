using System.Collections.Generic;
using BehaviorTree.SO.Actions;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskCallUltimateHypeNode : ActionNode
    {
        private TaskCallUltimateHypeNodeSO _so;
        private TaskCallUltimateHypeNodeDataSO _data;
        private EnemyManager _enemyManager;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskCallUltimateHypeNodeSO)nodeSO;
            _data = (TaskCallUltimateHypeNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            _enemyManager.CanUltimateEvent?.Invoke();
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _enemyManager = (EnemyManager)enemyDependencyValues[BehaviorTreeEnums.TreeEnemyValues.EnemyManager];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}