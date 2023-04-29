using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using HelperPSR.Collections;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskSetStringNode: ActionNode
    {
        private TaskSetStringNodeSO _so;
        private TaskSetStringNodeDataSO _data;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskSetStringNodeSO)nodeSO;
            _data = (TaskSetStringNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[0].HashCode, _data.StringValue);
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