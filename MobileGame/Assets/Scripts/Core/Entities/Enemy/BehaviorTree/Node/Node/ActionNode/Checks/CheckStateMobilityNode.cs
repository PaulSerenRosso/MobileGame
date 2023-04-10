using System.Collections.Generic;
using BehaviorTree.SO.Actions;

namespace BehaviorTree.Nodes.Actions
{
    public class CheckStateMobilityNode : ActionNode
    {
        private CheckStateMobilityNodeSO _so;
        private CheckStateMobilityNodeDataSO _data;
        private EnemyManager _enemyManager;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (CheckStateMobilityNodeSO)nodeSO;
            _data = (CheckStateMobilityNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            base.Evaluate();
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