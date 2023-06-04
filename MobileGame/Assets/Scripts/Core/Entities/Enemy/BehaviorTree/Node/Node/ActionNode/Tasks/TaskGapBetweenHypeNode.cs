using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using HelperPSR.Collections;
using Service.Hype;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskGapBetweenHypeNode : ActionNode
    {
        private TaskGapBetweenHypeNodeSO _so;
        private TaskGapBetweenHypeNodeDataSO _data;
        private IHypeService _hypeService;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskGapBetweenHypeNodeSO)nodeSO;
            _data = (TaskGapBetweenHypeNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            var gap = (_hypeService.GetCurrentHypeEnemy() + _hypeService.GetCurrentHypePlayer()) /
                        _hypeService.GetMaximumHype();
            CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[0].HashCode, gap);
            State = BehaviorTreeEnums.NodeState.SUCCESS;
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