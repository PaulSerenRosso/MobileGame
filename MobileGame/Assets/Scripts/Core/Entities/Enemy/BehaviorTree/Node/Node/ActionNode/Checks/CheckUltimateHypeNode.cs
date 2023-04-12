using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Service.Hype;

namespace BehaviorTree.Nodes.Actions
{
    public class CheckUltimateHypeNode : ActionNode
    {
        private CheckUltimateHypeNodeSO _so;
        private CheckUltimateHypeNodeDataSO _data;
        private EnemyManager _enemyManager;
        private IHypeService _hypeService;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (CheckUltimateHypeNodeSO)nodeSO;
            _data = (CheckUltimateHypeNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            if (_hypeService.GetUltimateAreaEnemy()) _enemyManager.CanUltimateEvent?.Invoke();
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _enemyManager = (EnemyManager)enemyDependencyValues[BehaviorTreeEnums.TreeEnemyValues.EnemyManager];
            _hypeService = (IHypeService)externDependencyValues[BehaviorTreeEnums.TreeExternValues.HypeService];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}