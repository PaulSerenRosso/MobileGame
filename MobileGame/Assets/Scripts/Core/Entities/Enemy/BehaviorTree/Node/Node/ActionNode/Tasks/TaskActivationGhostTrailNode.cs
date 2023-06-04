using System.Collections.Generic;
using BehaviorTree.SO.Actions;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskActivationGhostTrailNode : ActionNode
    {
        private TaskActivationGhostTrailNodeSO _so;
        private TaskActivationGhostTrailNodeDataSO _data;
        private EnemyManager _enemyManager;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskActivationGhostTrailNodeSO)nodeSO;
            _data = (TaskActivationGhostTrailNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            _enemyManager.ActivateGhostTrail(_data.Direction);
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