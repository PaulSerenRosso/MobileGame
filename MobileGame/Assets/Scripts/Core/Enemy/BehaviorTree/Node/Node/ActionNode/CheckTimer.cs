using System.Collections.Generic;
using BehaviorTree.ActionsSO;

namespace BehaviorTree.Nodes
{
    public class CheckTimer : ActionNode
    {
        private CheckTimerSO _timerSo;
        private CheckTimerDataSO _dataSO;
        private float _timer;

        public override NodeSO GetNodeSO()
        {
            return _timerSo;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _timerSo =(CheckTimerSO) nodeSO;
            _dataSO =(CheckTimerDataSO) _timerSo.Data;
        }

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