using System.Collections.Generic;
using BehaviorTree.ActionsSO;

namespace BehaviorTree.Nodes
{
    public class CheckTimer : ActionNode
    {
        private CheckTimerDataSO _dataSO;
        private float _timer;

        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            if (_timer > _dataSO.Time)
            {
                
            }

            return BehaviourTreeEnums.NodeState.FAILURE;
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _dataSO;
        }

        public override void SetDataSO(ActionNodeDataSO dataSO)
        {
            _dataSO = (CheckTimerDataSO)dataSO;
        }

        public override void SetDependencyValues(
            Dictionary<BehaviourTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviourTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
        }

        public override void SetHashCodeKeyOfInternValues(int[] hashCodeKey)
        {
        }
    }
}