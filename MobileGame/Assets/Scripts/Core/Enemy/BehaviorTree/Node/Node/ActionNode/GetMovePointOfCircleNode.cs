using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Environment.MoveGrid;

namespace BehaviorTree.Nodes.Actions
{
    public class GetMovePointOfCircleNode : ActionNode
    {
        private GetMovePointOfCircleNodeSO _getMovePointOfCircleNodeSO;
        private GetMovePointOfCircleNodeDataSO _getMovePointOfCircleNodeDataSO;
        private EnvironmentGridManager _environmentGridManager;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _getMovePointOfCircleNodeSO = (GetMovePointOfCircleNodeSO)nodeSO;
            _getMovePointOfCircleNodeDataSO = (GetMovePointOfCircleNodeDataSO)_getMovePointOfCircleNodeSO.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _getMovePointOfCircleNodeSO;
        }

        // Modulo de nombre de points dans le cercle * l'index du cercle + 1
        public override BehaviourTreeEnums.NodeState Evaluate()
        {
            int startIndex = (int)Sharer.InternValues[_getMovePointOfCircleNodeSO.StartIndexKey.HashCode];
            Sharer.InternValues[_getMovePointOfCircleNodeSO.ResultIndexKey.HashCode] = _environmentGridManager.GetIndexMovePointFromStartMovePointCircle(startIndex,
                _getMovePointOfCircleNodeDataSO.IndexMovedAmount);
            return BehaviourTreeEnums.NodeState.SUCCESS;
        }

        public override void SetDependencyValues(
            Dictionary<BehaviourTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviourTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _environmentGridManager =
                (EnvironmentGridManager)externDependencyValues[
                    BehaviourTreeEnums.TreeExternValues.EnvironmentGridManager];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _getMovePointOfCircleNodeDataSO;
        }
    }
}