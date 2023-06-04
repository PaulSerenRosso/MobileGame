using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Service.UI;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskActivateVibrationNode : ActionNode
    {
        private TaskActivateVibrationNodeSO _so;
        private TaskActivateVibrationNodeDataSO _data;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskActivateVibrationNodeSO)nodeSO;
            _data = (TaskActivateVibrationNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            Vibration.Vibrate(100);
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