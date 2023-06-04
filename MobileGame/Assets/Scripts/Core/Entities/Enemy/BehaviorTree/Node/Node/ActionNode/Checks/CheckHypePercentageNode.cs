using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Service.Hype;

namespace BehaviorTree.Nodes.Actions
{
    public class CheckHypePercentageNode : ActionNode
    {
        private CheckHypePercentageNodeSO _so;
        private CheckHypePercentageNodeDataSO _data;
        private IHypeService _hypeService;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (CheckHypePercentageNodeSO)nodeSO;
            _data = (CheckHypePercentageNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            State = _hypeService.GetCurrentHypeEnemy() / _hypeService.GetMaximumHype() < _data.PercentageCompare
                ? BehaviorTreeEnums.NodeState.SUCCESS
                : BehaviorTreeEnums.NodeState.FAILURE;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _hypeService = (IHypeService)externDependencyValues[BehaviorTreeEnums.TreeExternValues.HypeService];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}