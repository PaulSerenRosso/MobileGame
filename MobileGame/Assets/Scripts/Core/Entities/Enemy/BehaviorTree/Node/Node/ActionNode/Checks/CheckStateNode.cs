using System.Collections.Generic;
using BehaviorTree.SO.Actions;

namespace BehaviorTree.Nodes.Actions
{
    public class CheckStateNode : ActionNode
    {
        private CheckStateNodeSO _so;
        private CheckStateNodeDataSO _data;
        private EnemyManager _enemyManager;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (CheckStateNodeSO)nodeSO;
            _data = (CheckStateNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            State = _enemyManager.CurrentMobilityState == _data.enemyMobilityState
                ? BehaviorTreeEnums.NodeState.SUCCESS
                : BehaviorTreeEnums.NodeState.FAILURE;
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