using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using HelperPSR.Collections;
using Player.Handler;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskSendPlayerMovePointIndexNode : ActionNode
    {
        private TaskSendPlayerMovePointIndexNodeSO _so;
        private TaskSendPlayerMovePointIndexNodeDataSO _data;
        private PlayerMovementHandler _playerMovementHandler;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskSendPlayerMovePointIndexNodeSO)nodeSO;
            _data =
                (TaskSendPlayerMovePointIndexNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override IEnumerator<BehaviorTreeEnums.NodeState> Evaluate()
        {
            CollectionHelper.AddOrSet(ref Sharer.InternValues, _so.InternValues[0].HashCode, _playerMovementHandler.GetCurrentIndexMovePoint());

            yield return BehaviorTreeEnums.NodeState.SUCCESS;
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _playerMovementHandler =
                (PlayerMovementHandler)externDependencyValues[
                    BehaviorTreeEnums.TreeExternValues.PlayerHandlerMovement];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}