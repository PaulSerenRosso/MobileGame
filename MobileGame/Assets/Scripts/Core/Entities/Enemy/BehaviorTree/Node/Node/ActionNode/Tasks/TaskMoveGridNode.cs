using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Environment.MoveGrid;
using Player.Handler;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskMoveGridNode : ActionNode
    {
        private TaskMoveGridNodeSO _so;
        private TaskMoveGridNodeDataSO _data;
        private EnvironmentGridManager _environmentGridManager;
        private PlayerMovementHandler _playerMovementHandler;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskMoveGridNodeSO)nodeSO;
            _data = (TaskMoveGridNodeDataSO)_so.Data;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            _environmentGridManager.MoveGrid((Vector3)Sharer.InternValues[_so.InternValues[0].HashCode]);
            _playerMovementHandler.SetCurrentMovePoint((int)Sharer.InternValues[_so.InternValues[1].HashCode]);
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _environmentGridManager =
                (EnvironmentGridManager)externDependencyValues[
                    BehaviorTreeEnums.TreeExternValues.EnvironmentGridManager];
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