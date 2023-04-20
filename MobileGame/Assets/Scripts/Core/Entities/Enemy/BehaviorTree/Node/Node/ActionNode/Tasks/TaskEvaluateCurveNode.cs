using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using HelperPSR.Collections;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskEvaluateCurveNode : ActionNode
    {
        private TaskEvaluateCurveNodeSO _so;
        private TaskEvaluateCurveNodeDataSO _data;
        private EnemyManager _enemyManager;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskEvaluateCurveNodeSO)nodeSO;
            _data = (TaskEvaluateCurveNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            var ratio = (float)Sharer.InternValues[_so.InternValues[0].HashCode];
            CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[1].HashCode,
                _data.Curve.Evaluate(ratio));
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}